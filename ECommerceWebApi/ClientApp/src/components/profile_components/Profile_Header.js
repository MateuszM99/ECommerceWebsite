import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import '../../styles/profile_styles/profile__header__style.scss'

export class Profile_Header extends Component {
    render() {
        return (
            <div className="profile__header">
                <h3>My profile</h3>
                <Link to="/" className="profile__header__back__button">
                    <p>Back to shop</p>
                </Link>
            </div>
        )
    }
}

export default Profile_Header
