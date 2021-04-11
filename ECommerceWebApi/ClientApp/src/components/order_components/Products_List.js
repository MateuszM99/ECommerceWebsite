import React, { Component } from 'react'
import axios from 'axios'
import { CartContext } from '../../contexts/CartContext'

export class Products_List extends Component {

    static contextType = CartContext;

    constructor(props){
        super(props)

    }

    render() {
        const {cartProducts,cartPrice} = this.context;
        if(cartProducts == null){
            return(
                <div>Could not load content</div>
            )
        }
        return (
            <div className="products__list">
                    <div className="products__list__header">
                    <h4>{cartProducts.length} Items</h4>
                    </div>
                    {cartProducts.map(cartProduct => (
                    <div className="products__list__product" key={cartProduct.product.id}>
                        <img src={cartProduct.product.imageUrl}></img>
                        <span>
                            <p className="products__list__product__name">{cartProduct.product.name}</p>
                            <p className="products__list__product__size">Size: S</p>
                        </span>
                        <p className="products__list__product__quantity">{cartProduct.quantity}</p>
                        <p className="products__list__product__price">{cartProduct.product.price}$</p>
                    </div>
                    ))}
                    <div className="products__list__costs">
                        <span>
                            <p className="products__list__costs__label">Sum:</p>
                    <p className="products__list__costs__price">{cartPrice} $</p>
                        </span>
                        <span>
                            <p className="products__list__costs__label">Delivery:</p>
                            <p className="products__list__costs__price">{this.props.delivery} $</p>
                        </span>
                    </div>
                    <div className="products__list__total">
                        <p>Total:</p>
                        <p>{cartPrice + this.props.delivery} $</p>
                    </div>
                    <div className="order__comment">
                        <p>Add comment to the order:</p>
                        <textarea></textarea>
                    </div>
            </div>           
        )
    }
}

export default Products_List
