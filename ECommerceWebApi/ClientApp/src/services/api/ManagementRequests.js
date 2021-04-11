import axios from 'axios'
import FormData from 'form-data'

const baseUrl = 'https://localhost:44333/api';

export function getAvailableCategoriesRequest(){
    return axios.get(`${baseUrl}/products/getCategories`);
}

export function addProductRequest(values){
    console.log(values);
    const config = {
        headers: { 'content-type': 'multipart/form-data' }
    }
    let formData = new FormData();
    formData.append('productImage',values.image);
    formData.append('name',values.name);
    formData.append('sku',values.sku);
    formData.append('price',values.price);
    formData.append('categoryId',values.category);
    if(values.productId != "New product"){
        formData.append('productId',values.productId);
    }
    return axios.post(`${baseUrl}/products/addProduct`,formData,config);
}

export function deleteProductRequest(id,variationId){
    return axios.post(`${baseUrl}/products/deleteProduct`,{productId : id,productVariationId : variationId});
}

export function addStockToProductOptionRequest(productId,optionId,stock){
    return axios.post(`${baseUrl}/products/addStockToProductOption`,{productId : productId,optionId : optionId,stock:stock});
}

export function addCategoryRequest(values){
    return axios.post(`${baseUrl}/products/addCategory`,{name : values.name});
}

export function deleteCategoryRequest(id){
    return axios.post(`${baseUrl}/products/deleteCategory`,{id: id});
}

export function addOptionRequest(values){
    return axios.post(`${baseUrl}/products/addOption`,{name : values.name});
}

export function deleteOptionRequest(id){
    return axios.post(`${baseUrl}/products/deleteOption`,{id: id});
}

export function getCurrentOrdersRequest(){
    return axios.post(`${baseUrl}/order/getOrders`);
}

export function cancelOrderRequest(id){
    return axios.post(`${baseUrl}/order/cancelOrder?orderId=${id}`);
}

export function editOrderRequest(values){
    return axios.post(`${baseUrl}/order/editOrder`,values);
}
