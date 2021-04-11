import React,{ useEffect, useState } from 'react'
import '../../styles/cm_styles/cm__orders__style.scss'
import Order_tr from './Order_tr'
import axios from 'axios';
import { cancelOrderRequest, getCurrentOrdersRequest } from '../../services/api/ManagementRequests';

function Orders_Management() {

    const [orders,setOrders] = useState([]);
    const [error,setError] = useState(null);

    const getOrders = async () => {
        try{
            let response = await getCurrentOrdersRequest();
            console.log(response.data);
            setOrders(response.data);
        } catch (error) {
            console.log(error);
        }
    }


    useEffect(() => {
        getOrders();
    },[])

    const cancelOrder = async (id) => {
        try{
            let response = await cancelOrderRequest(id);
            console.log(response.data);
        } catch(error) {
            console.log(error);
        }
    } 


    return (
    <div className="cm__orders__container">
        <div className="cm__orders__container__filter">
            <label>Search : </label>
            <input></input>
        </div>
        <table>
            <thead>
                <tr>
                <th>ID</th>
                <th>Email</th>
                <th>Status</th>
                <th>Price</th>
                <th>Modified at</th>
                <th>Added at</th>
                <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {orders.map(order => (
                    <Order_tr key={order.id} id={order.id} email={order.clientEmail} status={order.status} price={order.price} modifiedAt={order.modifiedAt} addedAt={order.addedAt} cancelOrder = {cancelOrder}/>
                ))}
            </tbody>
        </table>
    </div>
    )
}

export default Orders_Management
