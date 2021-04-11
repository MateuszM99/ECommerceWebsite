import React, { Component } from 'react'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import axios from 'axios';
import {toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import '../../styles/login.scss'
import {Link,withRouter} from "../../../node_modules/react-router-dom"

export class Login extends Component {

    render() {
        return (
            <div className="login__container">
                <Link className="login__container__box__button__back" to="/">
                    <img src="images/left-arrow-16.png" className="login__container__box__button__back__img"></img>
                    Back to shop
                </Link>
                <Formik
                    initialValues={{
                       username : '',
                       password : '',
                    }}
                    validationSchema = {Yup.object({
                        username : Yup.string()
                            .required('Username is required'),
                        password : Yup.string()
                            .required('Password is required'),
                    })}

                    onSubmit = {(values,{setSubmitting, setStatus,resetForm}) => {
                        setTimeout(() => { 
                                if(values.username && values.password){
                                    axios.post("https://localhost:44333/api/authenticate/login",values)
                                    .then(response => {
                                        localStorage.setItem('userData',JSON.stringify(response.data));
                                        console.log(response.data);
                                        toast.success('Succesfully logged in', {
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
                                            setSubmitting(false);
                                            resetForm();
                                            setStatus({
                                                errorMessage : "Looks like either your username or password were incorrect or your email is not confirmed. Wanna try again?"
                                            });
                                        })                                                                      
                                }                       
                        },1000)
                    }} 
                >   
                    {({ errors, touched,isSubmitting,status}) => (
                    <Form>
                        <div className="login__container__box">                           
                            <p>Log in</p>             
                            <div className="login__container__box__input">
                            <label>Username</label>
                            <Field type="text" placeholder="Enter your username" name="username"></Field>
                            {errors.username && touched.username ? <div className="login-validation">{errors.username}</div> : null}
                            </div>
                            <div className="login__container__box__input">
                            <label>Password</label>
                            <Field type="password" placeholder="Enter your password" name="password"></Field>
                            {errors.password && touched.password ? <div className="login-validation">{errors.password}</div> : null}
                            </div>                           
                            <button className="login__container__box__button" type="submit">{isSubmitting ? 'Login in ...' : 'Log in'}</button> 
                            {status && status.errorMessage ? (
                                <div className="login-validation">{status.errorMessage}</div>
                            ) : null}            
                            <Link className="login__container__box__signup" to="/signup">Don't have account yet? Sign up</Link> 
                        </div>
                    </Form>
                    )}
                </Formik>
            </div>
        )
    }
}

export default (withRouter(Login))
