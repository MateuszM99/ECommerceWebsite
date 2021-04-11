import React from 'react'
import '../../styles/profile_styles/profile__tabs__style.scss';
import EditIcon from '@material-ui/icons/Edit';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import LockIcon from '@material-ui/icons/Lock';
import LocalPostOfficeIcon from '@material-ui/icons/LocalPostOffice';
import PaymentIcon from '@material-ui/icons/Payment';
import LocalOfferIcon from '@material-ui/icons/LocalOffer';
import { Link } from 'react-router-dom';




function Content_Managment_Nav_Tab() {    
    
    function onTabClick(id){
        let x;
        x = document.getElementsByClassName("profile__nav__tabs__tab");
        for(let i = 0; i < x.length; i++){
            x[i].className="profile__nav__tabs__tab"
        }

        x[id].className = "profile__nav__tabs__tab clicked"
    }

    return (
        <nav className="profile__nav__tabs">
            <Link className="profile__nav__tabs__tab" to="/manage/products" onClick={() => onTabClick(0)}>
                <LocalOfferIcon style={{ paddingLeft: '25px' }}/>
                <p className="profile__nav__tabs__tab__text">Manage Products</p>
            </Link>
            <Link className="profile__nav__tabs__tab" to="/manage/categories" onClick={() => onTabClick(1)}>
                <LocalOfferIcon style={{ paddingLeft: '25px' }}/>
                <p className="profile__nav__tabs__tab__text">Manage Categories</p>
            </Link>
            <Link className="profile__nav__tabs__tab" to="/manage/options" onClick={() => onTabClick(2)}>
                <LocalOfferIcon style={{ paddingLeft: '25px' }}/>
                <p className="profile__nav__tabs__tab__text">Manage Options</p>
            </Link>
            <Link className="profile__nav__tabs__tab" to="/manage/orders" onClick={() => onTabClick(3)}>
                <ShoppingCartIcon style={{ paddingLeft: '25px' }}/>
                <p className="profile__nav__tabs__tab__text">Manage Orders</p>
            </Link>
            {/* <Link className="profile__nav__tabs__tab" to="/manage/accounts" onClick={() => onTabClick(4)}>
                <LockIcon style={{ paddingLeft: '25px' }}/>
                <p className="profile__nav__tabs__tab__text">Manage Accounts</p>
            </Link> */}
        </nav>
    )
}

export default Content_Managment_Nav_Tab
