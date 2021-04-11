import React, { Component } from 'react'
import { Field } from 'formik'
import Delivery_Method from './Delivery_Method'
import Payment_Method from './Payment_Method'
import { getDeliveriesRequest, getPaymentsRequest } from '../../services/api/OrderRequests';

export class PaymentDeliveryForm extends Component {

    constructor(props){
        super(props);
        
        this.state = {
            payment_methods : [],
            delivery_methods : [],
            error: null,
            isLoaded : false
        }
    }

    async componentDidMount(){
        try{
            let deliveriesRequest = await getDeliveriesRequest();
            let paymentsRequest = await getPaymentsRequest();
            this.setState({
                delivery_methods : deliveriesRequest.data.deliveryMethods,
                payment_methods : paymentsRequest.data.paymentMethods,
                isLoaded : true
            })
        } catch(err){
            this.setState({
                error : err.response.data
            })
        }
    }
    


    render() {
        if(this.state.error != null){
            return (
                <div>Error</div>
            )
        }
        if(this.state.isLoaded == false){
            return (
                <div>Loading ...</div>
            )
        }
        const isChecked = this.state;
        const {payment_methods,delivery_methods} = this.state;
        return (
            <div className="order__form__payment__delivery">
                <span>     
                    <label>Delivery method:</label>
                    <div className="order__form__delivery__inputs">       
                            {delivery_methods.map(delivery_method => (
                                <Delivery_Method key={delivery_method.id} deliveryName={delivery_method.name} price={delivery_method.price} value={delivery_method.id} delivery={this.props.delivery} setFieldValue = {this.props.setFieldValue} handleDeliveryChange={this.props.handleDeliveryChange}/>
                                ))}                                   
                    </div>
                </span>
                {this.props.errors.delivery_method && this.props.touched.delivery_method ? <div className="validation">{this.props.errors.delivery_method}</div> : null}
                <span>     
                    <label>Payment method:</label>
                    <div className="order__form__payment__inputs">
                        {payment_methods.map(payment_method => (
                            <Payment_Method key={payment_method.id} value={payment_method.id} paymentName={payment_method.name} payment={this.props.payment} setFieldValue = {this.props.setFieldValue} handlePaymentChange={this.props.handlePaymentChange}/>
                        ))}
                    </div>
                </span>
                {this.props.errors.payment_method && this.props.touched.payment_method ? <div className="validation">{this.props.errors.payment_method}</div> : null}
            </div>
        )
    }
}

export default PaymentDeliveryForm
