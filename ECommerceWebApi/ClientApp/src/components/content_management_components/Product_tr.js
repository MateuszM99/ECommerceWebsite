import React from 'react'
import {Link} from 'react-router-dom'
import axios from 'axios';

function Product_tr(props) {  
    
    return (
        <tr>
            <td>{props.id}</td>
            <td><img src={props.image} className="cm__products__container__image"></img></td>
            <td>{props.name}</td>
            <td>{props.category}</td>
            <td>{props.sku}</td>
            <td>{props.price}</td>
            <td>{props.modifiedAt}</td>
            <td>{props.addedAt}</td>
            <td>
                <div className="cm__products__container__actions">
                    <Link className="cm__products__container__product__button" style={{width: "150px"}} to={`/manage/products/setOptionsStock/${props.id}`}>Set options/stock</Link>
                    <Link className="cm__products__container__product__button" to={`/manage/products/editProduct/${props.id}`}>Edit</Link> 
                    <button className="cm__products__container__product__button" onClick={() => props.delete(props.id,props.variationId)}>Delete</button>
                </div>
            </td>
        </tr>
    )
}

export default Product_tr
