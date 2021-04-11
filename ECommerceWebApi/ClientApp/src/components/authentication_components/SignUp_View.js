import React, { Component } from 'react'
import PostData from '../../services/data_requests/PostData'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import axios from 'axios'
import {toast} from 'react-toastify'
import SignUp from './SignUp'

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/
const passwordRegExp = /^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$/

export default class SignIn_View extends Component {
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
            <div className="signup__view__background">
                <div className="signup__view__container">    
                    <SignUp redirectPath={"/"}/>
                </div>
            </div>
        )
    }
}
