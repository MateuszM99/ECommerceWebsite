import React,{createContext,Component} from 'react';
import {addToNullCartRequest} from '../services/api/CartRequests';
import {addToCartRequest} from '../services/api/CartRequests';
import {removeItemFromCartRequest} from '../services/api/CartRequests';
import {getCartDataRequest} from '../services/api/CartRequests';
import { toast} from 'react-toastify';

export const CartContext = createContext();

class CartContextProvider extends Component {
    state = {
        cartProducts : [],
        cartPrice : 0,
        cartCount : 0,
        error : null
    }

    addToCart = async (productId,variationId,quantity=1,sizeOption="S") => {    
        if(localStorage.getItem("cartData") == null){
            let data = null;
            try {
                let response = await addToNullCartRequest(productId,variationId,quantity,sizeOption);
                data = response.data;
                toast.success(response.data.message);
            } catch(err) {
                console.log(err);
                toast.error(err.response.data);
            }

            if(data != null){
                localStorage.setItem("cartData",JSON.stringify(data));
                this.setState ({
                    cartProducts : data.cartCount,
                    cartPrice : data.cartPrice,
                    cartCount : data.cartCount
                })
            }
        } else {
            let cartData = JSON.parse(localStorage.getItem("cartData"));
            let cartId = cartData.cartId
            let data = null;
            try {
                let response = await addToCartRequest(cartId,productId,variationId,quantity,sizeOption);
                data = response.data;
                toast.success(response.data.message);
            } catch(err) {
                toast.error(err.response.data);
            }
           
            if(data != null){
                localStorage.setItem("cartData",JSON.stringify(data));
                this.setState ({
                    cartProducts : data.cartProducts,
                    cartPrice : data.cartPrice,
                    cartCount : data.cartCount
                })
            }
        }
    }

    removeItemFromCart = async (productId,variationId) => {
        if(localStorage.getItem("cartData") == null){
            toast.dark('Your cart is empty')    
            } else {
            let cartData = JSON.parse(localStorage.getItem("cartData"));
            let cartId = cartData.cartId;
            let data = null;
            try {
                let response = await removeItemFromCartRequest(cartId,productId,variationId);
                data = response.data;
                toast.success(response.data.message);
            } catch(err) {
                toast.error(err.response.data);
            }

            if(data != null){
                localStorage.setItem("cartData",JSON.stringify(data));
                this.setState ({
                    cartProducts : data.cartProducts,
                    cartPrice : data.cartPrice,
                    cartCount : data.cartCount
                })
            }
        }
    }

    async componentDidMount(){
        if(localStorage.getItem("cartData") != null){
            let cartData = JSON.parse(localStorage.getItem("cartData"));
            let cartId = cartData.cartId;
            let data = null;
            
            try {
                let response = await getCartDataRequest(cartId);
                data = response.data;
            } catch(err) {
                toast.error('Could not load cart data');
            }

            if(data != null){
                this.setState ({
                    cartProducts : data.cartProducts,
                    cartPrice : data.cartPrice,
                    cartCount : data.cartCount
                })
            }
        }
    }

    render() {
        return (
            <CartContext.Provider value={{...this.state,addToCart:this.addToCart,removeItemFromCart:this.removeItemFromCart}}>
                {this.props.children}
            </CartContext.Provider>
        )
    }
}

export default CartContextProvider;
