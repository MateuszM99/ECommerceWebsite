import { Field } from 'formik'
import React, { Component } from 'react'

export class Delivery_Method extends Component {
    render() {
        let isChecked;
        if(this.props.delivery == this.props.price){
        isChecked = true;
        } else {
        isChecked = false;    
        }
        return (
            <div className="order__form__delivery__inputs__input" style={isChecked ? {backgroundColor:"white",border:"solid 1px lightgray"} : {backgroundColor:"#f4f4f4",fontWeight:"400"}}>
                <Field type="radio" name="delivery_method" 
                value={this.props.value} 
                checked={isChecked}  
                onChange={e => {
                    this.props.handleDeliveryChange(this.props.price);
                    this.props.setFieldValue('delivery_method',e.target.value)
                }}
                />
                <div>
                <p>{this.props.deliveryName}</p>
                <p>{this.props.price} $</p>
                </div>
            </div>
        )
    }
}

export default Delivery_Method
