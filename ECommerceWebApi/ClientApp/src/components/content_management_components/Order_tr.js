import React from 'react'
import {Link} from 'react-router-dom'

function Order_tr(props) {
    return (
        <tr>
            <td>{props.id}</td>
            <td>{props.email}</td>
            <td>{props.status}</td>
            <td>{props.price}</td>
            <td>{props.modifiedAt}</td>
            <td>{props.addedAt}</td>
            <td>
                <div className="cm__products__container__actions">
                    <Link className="cm__products__container__product__button" to={`/manage/orders/editOrder/${props.id}`}>Edit</Link> 
                    <button className="cm__products__container__product__button" onClick={() => props.cancelOrder(props.id)}>Cancel</button>
                </div>
            </td>
        </tr>
    )
}

export default Order_tr
