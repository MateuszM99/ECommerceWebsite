import React, { Component } from 'react'
import '../../styles/product_styles/product__details.scss'
import {Link,withRouter} from  "react-router-dom"
import PostData from '../../services/data_requests/PostData'
import { CartContext } from '../../contexts/CartContext'

export class Product_View_Details extends Component {

    static contextType = CartContext;

    constructor(props){
        super(props)

        this.state = {
            product : null,
            isLoaded : false,
            error : null,
            quantity : 1,
            size : 'S',
        }
    }

    componentDidMount(){
        fetch("https://localhost:44333/api/products/getProduct?productId=" + this.props.match.params.productId)
          .then(res => res.json())
          .then(
            (result) => {
            console.log(result)
              this.setState({
                isLoaded: true,
                product: result
              });
            },
            (error) => {
              this.setState({
                isLoaded: true,
                error
              });
            }
          )
    }

    handleQuantityChange = (e) => {
        this.setState({quantity : e.target.value})
    }

    handleSizeChange = (e) => {
        this.setState({size : e.target.value})
    }

    render() {
        const {addToCart} = this.context;
        if(!this.state.isLoaded) {
            return <div>Loading</div>
          }

        if(this.state.isLoaded && !this.state.product) {
            return <div>Error {this.state.error}</div>
          }

        return (
            <div className="product__details__container">
                <div className="product__details__image">
                    <img src={this.state.product.imageUrl}></img>
                </div>
                <div className="product__details__info">
                    <div className="product__details__info__nav">
                        <Link style={{ textDecoration: 'none'}} to="/">Shop</Link>
                        <span>/</span>
                        <Link style={{ textDecoration: 'none' }} to={`/${this.state.product.categoryName}`}>{this.state.product.categoryName}</Link>
                        <span>/</span>
                        <p>{this.state.product.productName}</p>
                    </div>
                    <div className="product__details__info__header">
                        <h2>{this.state.product.name}</h2>
                        <p>{this.state.product.price}</p>
                    </div>
                    <div className="product__details__info__select">
                        <span>
                            <p>Size</p>
                            <select value={this.state.size} onChange={this.handleSizeChange}>
                                <option value="XS">XS</option>
                                <option value="S">S</option>
                                <option value="M">M</option>
                                <option value="L">L</option>
                                <option value="XL">XL</option>
                            </select>
                        </span>
                        <input type="text" value={this.state.quantity} onChange={this.handleQuantityChange}></input>
                        <button onClick={()=> addToCart(this.state.product.id,this.state.quantity,this.state.size)}>Add to cart</button>
                    </div>           
                    <div className="product__details__info__about">
                        <p>Description :</p>
                        <p style={{fontSize: '13px'}}>{this.state.product.description}</p>
                    </div>
                </div>
            </div>
        )
    }
}

export default (withRouter(Product_View_Details));
