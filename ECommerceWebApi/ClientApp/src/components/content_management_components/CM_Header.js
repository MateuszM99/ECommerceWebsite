import React from 'react'
import { Link } from 'react-router-dom'

function CM_Header() {
    return (
        <div className="profile__header">                
                <Link to="/" className="profile__header__back__button">
                    <p>Back to shop</p>
                </Link>
        </div>
    )
}

export default CM_Header
