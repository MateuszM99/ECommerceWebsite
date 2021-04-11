import React, { Component } from 'react'
import '../../styles/product_styles/product__style.scss'
import PostData from '../../services/data_requests/PostData'
import {Link} from  "react-router-dom"

export class Product extends Component{

    render(){
        return (
            <div className="product__card">
                <img src={this.props.image} alt=""></img>
                <div className="product__details">
                <Link to={`/category/${this.props.category}/${this.props.id}`}>{this.props.title}</Link>
                <p>{this.props.price} $</p>
                <button className="add__toCart" onClick={() => this.props.addToCart(this.props.id,this.props.variationId)}>Add to cart</button>
                </div>
            </div>
        )
    }
}

export default Product
