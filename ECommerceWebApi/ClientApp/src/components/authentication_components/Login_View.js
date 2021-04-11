import React, { Component } from 'react'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import axios from 'axios';
import {toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import Login from './Login';
import {Link,withRouter} from "../../../node_modules/react-router-dom"

export default class Login_View extends Component {

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
        if(this.state.isLoggedIn){
            return (
                <div style={{display : 'flex', width : '100%', height : '100vh', justifyContent : 'center', alignItems : 'center'}}>
                    You are already logged in
                </div>
            )
        } 
        return (
            <div className="login__view__background">
                <div className="login__view__container">
                    <Login redirectPath={"/"}/>
                </div>
            </div>
        )
    }
}
