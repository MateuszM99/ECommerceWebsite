import { Field } from 'formik'
import React, { Component } from 'react'


export class Payment_Method extends Component {
   
    constructor(props){
        super(props);

    }



    render() {
        let isChecked;
        if(this.props.payment == this.props.value){
        isChecked = true;
        } else {
        isChecked = false;    
        }
        return (
            <div className="order__form__payment__inputs__input" style={isChecked ? {backgroundColor:"white",border:"solid 1px lightgray"} : {backgroundColor:"#f4f4f4",fontWeight:"400"}}>
                <Field type="radio" name="payment_method" 
                value = {this.props.value} 
                checked = {isChecked} 
                onChange = {e => {
                    this.props.handlePaymentChange(e);
                    this.props.setFieldValue('payment_method', e.target.value);
                }}/>
                <div>
                <p>{this.props.paymentName}</p>
                </div>
            </div>
        )
    }
}

export default Payment_Method
