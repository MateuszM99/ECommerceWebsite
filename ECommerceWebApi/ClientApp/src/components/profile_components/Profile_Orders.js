import React,{ useEffect, useState } from 'react'
import Profile_Order_TR from './Profile_Order_TR';
import { getUsersOrders } from '../../services/api/OrderRequests';

function Profile_Orders() {
    const [orders,setOrders] = useState([]);
    const [error,setError] = useState(null);

    const getOrders = async () => {
        try{
            let response = await getUsersOrders();
            console.log(response.data);
            setOrders(response.data);
        } catch (error) {
            console.log(error);
        }
    }


    useEffect(() => {
        getOrders();
    },[])

    return (        
        <div className="profile__orders__container">
            <div className="profile__orders__container__filter">
            <label>Search : </label>
            <input></input>
            </div>
            <table>
                <thead>
                    <tr>
                    <th>ID</th>                    
                    <th>Status</th>
                    <th>Price</th>
                    <th>Modified at</th>
                    <th>Added at</th>                   
                    </tr>
                </thead>
                <tbody>
                {orders.map(order => (
                    <Profile_Order_TR key={order.id} id={order.id} status={order.status} price={order.price} modifiedAt={order.modifiedAt} addedAt={order.addedAt} />
                ))}
                </tbody>
            </table>
        </div>
    )
}

export default Profile_Orders
