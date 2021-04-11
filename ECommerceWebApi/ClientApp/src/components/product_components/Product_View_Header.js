import React, { Component } from 'react'
import '../../styles/product_styles/product__view__header__style.scss'
import { withRouter } from "react-router-dom";
import Payment_Method from '../order_components/Payment_Method';
import Slider from '@material-ui/core/Slider'
import { withStyles } from '@material-ui/core/styles';
import Product from '../product_components/Product';

const styles = {
    root : {
        color : 'black',
        width : 150,
    } 
}

export class Product_View_Header extends Component {

    constructor(props){
        super(props);
        
        this.state = {
            sortState : this.grabSortValue(),
            orderState : this.grabQueryValue('orderType'),
            sizeState : this.grabQueryValue('size'),
            colorState : null,
            value : [this.grabQueryValue('priceFrom'),this.grabQueryValue('priceTo')],
            isPriceShown : false
        }
    }

    classes = this.props.classes;

    grabSortValue = () => {
        let value = new URLSearchParams(this.props.location.search).get('orderType');
        let sortType = new URLSearchParams(this.props.location.search).get('sortType');
        if(value != null){
            sortType = sortType.concat('Desc');
        }
        return sortType;
    }

    grabQueryValue = (key) => {
        let value = new URLSearchParams(this.props.location.search).get(key);
        return value;
    }

    addQuery = (key,value,searchParams) => {
        searchParams.set(key,value);
        return searchParams;
    }
    
    removeQuery = (key,searchParams) => {
        searchParams.delete(key);
        return searchParams;
    }
    
    handleSortChange = (e) => {
        let url = this.props.location;
        let searchParams = new URLSearchParams(url.search);

        if(e.target.value == ""){
            this.setState({sortState : null});
            searchParams = this.removeQuery("sortType", searchParams);
        } else {
            this.setState({sortState : e.target.value});
            searchParams = this.addQuery("sortType", e.target.value.replace("Desc",""), searchParams);         
        }

        if(/Desc$/.test(e.target.value)){
            let state = "desc"
            this.setState({orderState: state});
            searchParams = this.addQuery("orderType",state, searchParams);
        } else {
            this.setState({orderState: null});
            searchParams = this.removeQuery("orderType", searchParams);
        }

        this.props.history.push({
            pathname: url.pathname,
            search: searchParams.toString()
        })
    }

    handleSizeChange = (e) => {
        let url = this.props.location;
        let searchParams = new URLSearchParams(url.search);
        
        if(e.target.value == ""){
            this.setState({sizeState : null});
            searchParams = this.removeQuery("size", searchParams);
        } else {
            this.setState({sizeState : e.target.value});
            searchParams = this.addQuery("size", e.target.value, searchParams);
        }

        this.props.history.push({
            pathname: url.pathname,
            search: searchParams.toString()
        })
    }

    handlePriceChange = (event, value) => {
        this.setState({value : value});
    };

    handlePriceShow = () => {
        this.setState({isPriceShown: !this.state.isPriceShown})
    }

    handlePriceRangeSet = () => {
        let url = this.props.location;
        let searchParams = new URLSearchParams(url.search);
        
        if(this.state.value[0] != 0)
        searchParams = this.addQuery("priceFrom",this.state.value[0],searchParams);
        else {
            searchParams = this.removeQuery("priceFrom",searchParams);
        }

        if(this.state.value[1] != 999)
        searchParams = this.addQuery("priceTo",this.state.value[1],searchParams);
        else {
            searchParams = this.removeQuery("priceTo",searchParams);
        }

        this.props.history.push({
            pathname: url.pathname,
            search: searchParams.toString()
        })

        this.handlePriceShow();
    }


    render() {
        return (
            <div className="product__view__header">
                <p>{this.props.match.params.category}</p>
                <div className="sorting__filters">
                    <span>
                        <label>Sort: </label>
                        <select value={this.state.sortState} onChange={this.handleSortChange}>
                            <option value="">None</option>
                            <option value="price">Sort by price</option>
                            <option value="priceDesc">Sort by price descending</option>
                            <option value="name">Sort by name</option>
                            <option value="nameDesc">Sort by name descending</option>
                        </select>
                    </span>
                    <span>
                        <label>Size: </label>
                        <select value={this.state.sizeState} selected={this.state.sizeState} onChange={this.handleSizeChange}>
                            <option value="">None</option>
                            <option value="XS">XS</option>
                            <option value="S">S</option>
                            <option value="M">M</option>
                            <option value="L">L</option>
                            <option value="XL">XL</option>
                        </select>
                    </span>                   
                    <span>
                        <label>Price: </label>
                        <button className="display__price__set__button" onClick={this.handlePriceShow}>Set price</button>
                        <div className="set__price__container" style={{display : this.state.isPriceShown ? 'block' : 'none'}}>
                            <Slider
                            className={this.classes.root}
                            value={this.state.value}
                            onChange={this.handlePriceChange}
                            valueLabelDisplay="auto"
                            aria-labelledby="range-slider"
                            max={999}
                            />
                            <div className="box__value">
                                <span>{this.state.value[0]} $</span>
                                <span>-</span>
                                <span>{this.state.value[1]} $</span>
                            </div>
                            <div className="set__price__filter_container">
                            <button className="set__price__filter__button" onClick={this.handlePriceRangeSet}>Set</button>
                            </div>
                        </div>
                    </span>
                </div>
            </div>
        )
    }
}

export default withStyles(styles)(withRouter(Product_View_Header));
