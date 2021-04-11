import React, { Component } from 'react'
import '../../styles/main_styles/header__style.scss';
import Cart_View from '../cart_components/Cart_View';
import { Link } from "react-router-dom";
import { withRouter } from "react-router-dom";
import axios from 'axios'
import Badge from '@material-ui/core/Badge';
import IconButton from '@material-ui/core/IconButton';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart'; 
import { withStyles } from '@material-ui/core';
import { toast,ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import PaymentDeliveryForm from '../order_components/PaymentDeliveryForm';
import { CartContext } from '../../contexts/CartContext';

if(localStorage.getItem('userData') != null){
  var userData = JSON.parse(localStorage.getItem('userData'));
}


export class Header extends Component {
    
    static contextType = CartContext;

    constructor(props) {
        super(props);
    
        this.state = {
          isLoginShown : false,
          isSignInShown : false,
          isCartShown: false,
          isLoggedIn : false,
          cartCount : 0,
          categories : [],
          error: null,
          search: '',
          scrolled : false,
        };

        this.handleCartShow = this.handleCartShow.bind(this);
        this.handleLoggedIn = this.handleLoggedIn.bind(this);
        this.logout = this.logout.bind(this);
    }

    handleCartShow = () => {
        this.setState({isCartShown : !this.state.isCartShown});
    }

    handleLoginShow = () => {
        this.setState({isLoginShown : !this.state.isLoginShown});
    }

    handleSignInShow = () => {
        this.setState({isSignInShown : !this.state.isSignInShown});
    }

    handleLoggedIn = () => {
        console.log(localStorage.getItem("userData"))
        if(localStorage.getItem("userData") != null)
        this.setState({isLoggedIn : true})
    }

    logout = () => {
        localStorage.setItem("userData",'')
        localStorage.clear();
        this.setState({isLoggedIn: false})
    }

    addQuery = (key,value,searchParams) => {
      searchParams.set(key,value);
      return searchParams;
    }
  
    removeQuery = (key,searchParams) => {
        searchParams.delete(key);
        return searchParams;
    }

    handleSearchChange = (e) => {
        this.setState({search : e.target.value})
    }

    searchProduct = () => {
      let url = this.props.location;
      let searchParams = new URLSearchParams(url.search);

        if(this.state.search == ""){
            this.setState({search : ''});
            searchParams = this.removeQuery("productName", searchParams);
        } else {
            searchParams = this.addQuery("productName", this.state.search, searchParams);
        }

        this.props.history.push({
            pathname: url.pathname,
            search: searchParams.toString()
        })
    }

    
    getCartData = () => {
      let cartData = JSON.parse(localStorage.getItem("cartData"));
      if(cartData == null){
        this.setState({cartCount: 0});
      }

      if(cartData != null) {
        let cartCount = cartData.cartCount;
        this.setState({cartCount : cartCount})
      }
    }

    getCategoriesData = () => {
      fetch("https://localhost:44333/api/products/getCategories")
        .then(res => res.json())
        .then(
          (result) => {
            this.setState({
              categories : result
            });
          },
          (error) => {
            this.setState({
              error
            });
          }
        )
    }

    componentDidMount(){       
        this.handleLoggedIn();      
        this.getCartData();
        this.getCategoriesData();     
        }

    componentDidUpdate(){
      
    }
    

    render() {
      const {categories} = this.state;
        return (
            <header className="header active">   
              <div className="main__header">
                  <div className="header__logo">            
                  </div>
                  <div className="search__input">
                  <input type="text"  placeholder="Search for your product ..." onChange={this.handleSearchChange}></input>      
                  <button onClick={this.searchProduct}>Search</button>          
                  </div>
                  <div className="right__header">
                  <Link style={{display : this.state.isLoggedIn ? 'none' : 'block'}} to="/login">Log in</Link>                
                  <Link style={{display : this.state.isLoggedIn ? 'none' : 'block'}} to="/signup">Sign up</Link>                    
                  <Link style={{display : this.state.isLoggedIn ? 'block' : 'none'}} to="/profile">{userData ? userData.user.userName : null}</Link>
                  <a onClick={this.logout} style={{display : this.state.isLoggedIn ? 'block' : 'none'}}>Logout</a>   
                  <div className="cart__div">   
                  <IconButton aria-label="cart" onClick={this.handleCartShow}>
                  <Badge badgeContent={this.context.cartCount} color="secondary"
                    anchorOrigin={{
                      vertical: 'bottom',
                      horizontal: 'right',
                    }}
                  >
                  <ShoppingCartIcon />
                  </Badge>
                  </IconButton>
                  </div> 
                  <Cart_View isCartShown={this.state.isCartShown} onCartShow={this.handleCartShow}/>
                  </div>
              </div>
              <div className="main__categories">
                <Link to={`/`}>Home</Link>
                {categories.map(category => (                
                    <Link key={category.id} to={`/category/${category.name}`} style={{ textDecoration: 'none' }}>{category.name}</Link>                
                ))}
              </div>
            </header>
        )
    }
}

export default (withRouter(Header))
