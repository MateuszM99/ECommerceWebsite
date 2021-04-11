import React, { Component } from 'react'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import axios from 'axios';
import {toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import '../../styles/signup.scss'
import {Link,withRouter} from "../../../node_modules/react-router-dom"


const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/
const passwordRegExp = /^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$/

export class SignUp extends Component {

    constructor(props) {
        super(props);

        this.state = {
          error: null,
          isLoggedIn : false
        };
      }

    componentDidMount(){
        if(localStorage.getItem('userData') != null){
            this.setState({
                isLoggedIn : true
            })
        }
    }

    render() {
        return (
            <div className="signup__container">   
                <Link className="signup__container__button__back" to="/">
                    <img src="images/left-arrow-16.png" className="signup__container__box__button__back__img"></img>
                    Back to shop
                </Link> 
                <Formik
                    initialValues={{
                        firstName : '',
                        lastName : '',
                        username : '',
                        email : '',
                        phone : '',
                        password : '',
                    }}
                    validationSchema = {Yup.object({
                        firstName : Yup.string()
                            .required('First name is required'),
                        lastName : Yup.string()
                            .required('Last name is required'),
                        username : Yup.string()
                            .required('Username is required'),
                        email : Yup.string()
                            .email('Invalid email')
                            .required('Email is required'),
                        phone : Yup.string()
                            .matches(phoneRegExp,'Phone number is not valid')
                            .required('Phone number is required'),
                        password : Yup.string()
                            //.matches(passwordRegExp,'Password has to be at least 8 characters long,contain 1 special sign and 1 uppercase letter')
                            .required('Password is required'),
                    })}

                    onSubmit = {(values,{setSubmitting,setStatus,resetForm}) => {
                        setTimeout(() => {
                                axios.post("https://localhost:44333/api/authenticate/register",values)
                                        .then(response => {
                                            toast.success(response.data.message, {
                                                position: "top-right",
                                                autoClose: 2000,
                                                hideProgressBar: true,
                                                closeOnClick: true,
                                                pauseOnHover: true,
                                                draggable: true,
                                                progress: undefined,
                                                });
                                            setSubmitting(false);
                                            resetForm();    
                                            this.props.history.push({
                                                pathname : this.props.redirectPath
                                            })
                                        })
                                        .catch(error => {
                                            console.log(error);
                                            setSubmitting(false);
                                            setStatus({
                                                errorMessage : error.response.data.message
                                            });
                                        })    
                        },1000)
                    }} 
                >
                    {({ errors, touched,status,isSubmitting }) => (
                    <Form>  
                        <div className="signup__container__box">
                            <p>Sign up</p>
                            <div className="signup__container__box__input">
                            <label>First name</label>
                            <Field type="text" placeholder="Enter your first name" name="firstName"></Field>      
                            {errors.firstName && touched.firstName ? <div className="signup__container__box__validation">{errors.firstName}</div> : null}             
                            </div>
                            <div className="signup__container__box__input">
                            <label>Last name</label>
                            <Field type="text" placeholder="Enter your last name" name="lastName"></Field> 
                            {errors.lastName && touched.lastName ? <div className="signup__container__box__validation">{errors.lastName}</div> : null}               
                            </div>       
                            <div className="signup__container__box__input">
                            <label>Username</label>
                            <Field type="text" placeholder="Enter your username" name="username"></Field>  
                            {errors.username && touched.username ? <div className="signup__container__box__validation">{errors.username}</div> : null}              
                            </div>
                            <div className="signup__container__box__input">
                            <label>E-mail</label>
                            <Field type="text" placeholder="Enter your email" name="email"></Field>
                            {errors.email && touched.email ? <div className="signup__container__box__validation">{errors.email}</div> : null}
                            </div>
                            <div className="signup__container__box__input">
                            <label>Phone Number</label>
                            <Field type="text" placeholder="Enter your phone number" name="phone"></Field>
                            {errors.phone && touched.phone ? <div className="signup__container__box__validation">{errors.phone}</div> : null}
                            </div>
                            <div className="signup__container__box__input">
                            <label>Password</label>
                            <Field type="password" placeholder="Enter your password" name="password"></Field>
                            {errors.password && touched.password ? <div className="signup__container__box__validation">{errors.password}</div> : null}
                            </div>
                            <button className="signup__container__box__button" type="submit">{isSubmitting ? 'Signin up ...' : 'Sign up'}</button>
                            {status && status.errorMessage ? (
                                    <div className="signup__container__box__validation">{status.errorMessage}</div>
                                ) : null}
                            <Link className="signup__container__box__login" to="/login">Already have an account? Login</Link>
                        </div>
                    </Form>
                    )}
                </Formik>
            </div>
        )
    }
}

export default (withRouter(SignUp))
