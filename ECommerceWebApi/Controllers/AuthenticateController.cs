using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ECommerceContext appDb;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSender emailSender, ECommerceContext appDb)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appDb = appDb;
            _configuration = configuration;
            this.emailSender = emailSender;
        }

        public IActionResult getSessionToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return Ok(new
            {
                token       
            });
        }


        [EnableCors("Policy")]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.Users.Include(u => u.Address).SingleAsync(u => u.UserName == model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(24),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    user,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User with that username already exists!" });

            var userEmailExists = await userManager.FindByEmailAsync(model.Email);
            if(userEmailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User with that email already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Address = new Address()
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Authenticate", new { userId = user.Id, token = token }, Request.Scheme);
            string message = $"Click this link to confirm your account: {confirmationLink}";
           
            await emailSender.SendEmailAsync(model.Email, "Confirm your account", message);

            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }
   
        [HttpPost]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
                return StatusCode(StatusCodes.Status404NotFound, "No user found or token is invalid");

            var user = await userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,"User not found");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return Ok();

            return StatusCode(StatusCodes.Status404NotFound, "");
        }

        [HttpPost]
        [Route("sendConfirm")]
        public async Task<IActionResult> SendConfirmationMail(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Authenticate", new { userId = user.Id, token = token },Request.Scheme);
            string message = $"Click this link to confirm your account: " + confirmationLink;

            await emailSender.SendEmailAsync(user.Email, "Confirm your account", message);

            return Ok(new AuthResponse { Status = "Success", Message = "Message sent!" });
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Authorize]
        [Route("editUser")]
        public async Task<IActionResult> EditUserInfo([FromBody] EditUserModel userModel, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User doesn't exist!" });

            if (userModel.FirstName != null && userModel.FirstName != "")
                user.FirstName = userModel.FirstName;
            if (userModel.LastName != null && userModel.LastName != "")
                user.LastName = userModel.LastName;
            if (userModel.Email != null && userModel.Email != "")
                user.Email = userModel.Email;
            if (userModel.Phone != null && userModel.Phone != "")
                user.PhoneNumber = userModel.Phone;

            await userManager.UpdateAsync(user);

            return Ok(new { user, response = new AuthResponse { Status = "Success", Message = "Changes saved successfully" } });
        }

        
        [HttpPost]
        [Authorize]
        [Route("editAddress")]
        public async Task<IActionResult> EditAddressInfo([FromBody]Address addressModel, string username)
        {
            var user = await userManager.Users.Include(u => u.Address).SingleAsync(u => u.UserName == username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User doesn't exist!" });
                
            if (addressModel.Street != null && addressModel.Street != "")
                user.Address.Street = addressModel.Street;
            if (addressModel.Country != null && addressModel.Country != "")
                user.Address.Country = addressModel.Country;
            if (addressModel.City != null && addressModel.City != "")
                user.Address.City = addressModel.City;
            if (addressModel.PostCode != null && addressModel.PostCode != "")
                user.Address.PostCode = addressModel.PostCode;
            if (addressModel.HouseNumber != null && addressModel.HouseNumber != "")
                user.Address.HouseNumber = addressModel.HouseNumber;

            await userManager.UpdateAsync(user);

            return Ok(new { user, response = new AuthResponse { Status = "Success", Message = "Changes saved successfully" } });
        }


    }
}
