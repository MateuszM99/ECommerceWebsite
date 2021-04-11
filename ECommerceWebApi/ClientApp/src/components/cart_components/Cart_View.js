import React, { Component } from 'react'
import Cart_Product from '../cart_components/Cart_Product'
import '../../styles/cart_styles/cart__style.scss'
import {Link} from  "react-router-dom"
import axios from 'axios';
import { CartContext } from '../../contexts/CartContext';

export class Cart_View extends Component {

    static contextType = CartContext;

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
        const { error, isLoaded,isLoggedIn } = this.state;
        const {cartProducts,cartPrice,removeItemFromCart} = this.context;
        
        return (
            <div className="popup" style={{display : this.props.isCartShown ? 'block' : 'none',opacity : this.props.isCartShown ? '1' : '0'}}>
                <div className="cart__view">
                    <div className="cart__header">
                    <p>Cart</p>
                    <img src="/images/cross16.png" onClick={this.props.onCartShow} className="close__button"></img>
                    </div>
                    <ul className="cart__products">
                    {Array.isArray(cartProducts) ? cartProducts.map(cartProduct => (
                    <li key={cartProduct.product.id,cartProduct.product.variationId}>
                    <Cart_Product id={cartProduct.product.id} variationId={cartProduct.product.variationId} name={cartProduct.product.name} price={cartProduct.product.price} quantity={cartProduct.quantity} image={cartProduct.product.imageUrl} size={cartProduct.option.name} removeItemFromCart={removeItemFromCart}/>
                    </li>
                    )) : null}
                    </ul>
                    <div className="cart__total">
                    <p>Total price :</p>
                    <p>{cartPrice} $</p>
                    </div>
                    <div className="cart__order">
                    <a onClick={this.props.onCartShow}>Continue</a>
                    { isLoggedIn ? 
                    <Link to="/order" className="order__button" style={{display : this.context.cartCount != 0 ? 'flex' : 'none'}}>Order</Link> :
                    <Link to="/checkLogin" className="order__button" style={{display : this.context.cartCount != 0 ? 'flex' : 'none'}}>Order</Link>
                    }
                    </div>
                </div>
            </div>
        )
    }
}

export default Cart_View
