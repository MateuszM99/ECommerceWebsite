import React, { Component } from 'react'
import {Link} from "../../../node_modules/react-router-dom"
import Login from "./Login"
import SignUp from "./SignUp"
import "../../styles/check__login.scss"


export class CheckLogin extends Component {
    render() {
        return (
        <div className="check__login__main">   
            <div className="check__login__container">
                <div className="check__login__user">
                    <Login redirectPath={"/order"}/>
                </div>
                <div className="check__login__guest">
                    <Link to="/order">Continue as guest</Link>
                </div>
            </div>
        </div>     
        )
    }
}

export default CheckLogin
