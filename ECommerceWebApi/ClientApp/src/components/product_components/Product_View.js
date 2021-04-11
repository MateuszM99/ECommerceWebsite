import React, { Component } from 'react'
import Product from '../product_components/Product'
import '../../styles/product_styles/product__view__main.scss'
import { withRouter } from "react-router-dom";
import { CartContext } from '../../contexts/CartContext';

export class Product_View extends Component {
    
    static contextType = CartContext;

    constructor(props) {
        super(props);

        this.state = {
          error: null,
          isLoaded: false,
          products: [],
          searchQuery : "",
          category : undefined,
        };
      }

      addQuery = (key,value,searchParams) => {
        searchParams.set(key,value);
        return searchParams;
      }
    
    removeQuery = (key,searchParams) => {
        searchParams.delete(key);
        return searchParams;
    }

      getProductsData = () => {        
        let searchParams = new URLSearchParams(this.props.location.search);
        this.setState({category : undefined});
        if(this.props.match.params.category != null){
          console.log('goes there');
          searchParams = this.removeQuery("categoryName",searchParams);
          searchParams = this.addQuery("categoryName",this.props.match.params.category,searchParams);
          this.setState({category : this.props.match.params.category});
        }
        this.setState({searchQuery : this.props.location.search});
        fetch("https://localhost:44333/api/products/products?" + searchParams)
          .then(res => res.json())
          .then(
            (result) => {
              this.setState({
                isLoaded: true,
                products: result
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

      componentDidMount() {
        this.getProductsData();
      }

      componentDidUpdate(){
        if(this.state.searchQuery != this.props.location.search || (this.state.category != this.props.match.params.category))
        this.getProductsData();
      }
      

    render() {
        const { error, isLoaded, products } = this.state;
        const {addToCart} = this.context;
        return (
            <section id="products__list">
                <div className="product__main">
                    <ul>
                    {products.map(product => (
                    <li key={product.sku}>
                    <Product id={product.id} variationId={product.variationId} title={product.name} price={product.price} image={product.imageUrl} category={product.categoryName} addToCart={addToCart}/>
                    </li>
                    ))}
                    </ul>
                </div>
            </section>
        )
    }
}

export default (withRouter(Product_View))
