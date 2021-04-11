import axios from 'axios'

const baseUrl = 'https://localhost:44333/api';

export function getUsersOrders(){
    return axios.get(`${baseUrl}/order/getUserOrders`);
}

export function getDeliveriesRequest(){
    return axios.get(`${baseUrl}/order/getDeliveries`);
}

export function getPaymentsRequest(){
    return axios.get(`${baseUrl}/order/getPayments`);
}