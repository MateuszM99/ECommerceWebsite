import { toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import axios from 'axios'

const baseUrl = 'https://localhost:44333/api';

export function addToNullCartRequest(productId,variationId,quantity=1,sizeOption="S"){
    return axios.post(`${baseUrl}/cart/addCart`,{productId : productId,productVariationId : variationId,quantity: quantity,optionName : sizeOption});
}

export function addToCartRequest(cartId,productId,variationId,quantity=1,sizeOption="S"){
    return axios.post(`${baseUrl}/cart/addCart`,{cartId : cartId,productId : productId,productVariationId : variationId,quantity: quantity,optionName : sizeOption});
}

export function removeItemFromCartRequest(cartId,productId,variationId){
    return axios.post(`${baseUrl}/cart/removeFromCart`,{cartId : cartId, productId : productId, productVariationId : variationId});
}

export function getCartDataRequest(cartId){
    return axios.get(`${baseUrl}/cart/getCart?cartId=` + cartId);
}






