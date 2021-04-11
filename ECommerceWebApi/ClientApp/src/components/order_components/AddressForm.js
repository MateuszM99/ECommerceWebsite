import { Field } from 'formik'
import React, { Component } from 'react'
import CountrySelectList from '../CountrySelectList'

export class AddressForm extends Component {

    constructor(props){
        super(props);

        this.state = {
            isLoggedIn : false,
            hasAddress : false,
            isChecked : false,
        }
    }

    componentDidMount(){
        if(localStorage.getItem('userData') != null){
            this.setState({
                isLoggedIn : true
            })

            if(JSON.parse(localStorage.getItem('userData')).user.address != null){
                this.setState({
                    hasAddress : true,
                    isChecked : false
                })

                const userAddressData = JSON.parse(localStorage.getItem('userData')).user.address;

                this.props.setFieldValue('country',userAddressData.country);
                this.props.setFieldValue('city',userAddressData.city);
                this.props.setFieldValue('street',userAddressData.street);
                this.props.setFieldValue('houseNumber',userAddressData.houseNumber);
                this.props.setFieldValue('postCode',userAddressData.postCode);
            }
        }
    }

    checkBooleanValues = (value1,value2,value3) => {
        if((value1 === value2) && (value2 === value3)){
            return true;
        } else {
            return false;
        }
    }

    onCheckboxChange = () => {

        if(!this.state.isChecked){
            this.props.setFieldValue('country','');
                    this.props.setFieldValue('city','');
                    this.props.setFieldValue('street','');
                    this.props.setFieldValue('houseNumber','');
                    this.props.setFieldValue('postCode','');
        } else {
            if(localStorage.getItem('userData') != null){
                if(JSON.parse(localStorage.getItem('userData')).user.address != null){
                    const userAddressData = JSON.parse(localStorage.getItem('userData')).user.address;

                    this.props.setFieldValue('country',userAddressData.country);
                    this.props.setFieldValue('city',userAddressData.city);
                    this.props.setFieldValue('street',userAddressData.street);
                    this.props.setFieldValue('houseNumber',userAddressData.houseNumber);
                    this.props.setFieldValue('postCode',userAddressData.postCode);
                }
            }
        }

        this.setState({
            isChecked : !this.state.isChecked
        })
    }

    render() {
        return (
            <div>    
                <h3 className="order__form_header">Address details</h3>
                <div className="order_user_check" style={{display : this.state.isLoggedIn && this.state.hasAddress ? 'flex' : 'none'}}>
                <p>Enter custom address details</p>
                <input type="checkbox" onChange={this.onCheckboxChange}></input>
                </div>
                <div className="order__form__inputs" style={{display : (() => this.checkBooleanValues(this.state.isChecked,this.state.isLoggedIn,this.state.hasAddress)) ? 'block' : 'none'}}>
                    <div className="order__form__inputs__row">
                        <span>
                            <label>Country:</label>
                            <div>
                                <CountrySelectList name="country" class={"order__form__inputs__row_input_width"} style={{width: "400px"}}/>
                                {this.props.errors.country && this.props.touched.country ? <div className="validation">{this.props.errors.country}</div> : null}
                            </div>
                        </span>
                        <span>
                            <label>Post-Code:</label>
                            <div>
                                <Field type="text" name="postCode" className="order__form__inputs__row_input_width" style={{width: "125px"}}></Field>
                                {this.props.errors.postCode && this.props.touched.postCode ? <div className="validation">{this.props.errors.postCode}</div> : null}
                            </div>
                        </span>
                    </div>
                    <div className="order__form__inputs__row">
                        <span>
                            <label>City:</label>
                            <div>
                                <Field type="text" name="city" className="order__form__inputs__row_input_width2"></Field>
                                {this.props.errors.city && this.props.touched.city ? <div className="validation">{this.props.errors.city}</div> : null}
                            </div>
                        </span>
                    </div>
                    <div className="order__form__inputs__row">
                        <span>
                            <label>Street:</label>
                            <div>
                                <Field type="text" name="street" className="order__form__inputs__row_input_width" style={{width: "400px"}}></Field>
                                {this.props.errors.street && this.props.touched.street ? <div className="validation">{this.props.errors.street}</div> : null}
                            </div>
                        </span>
                        <span>
                            <label>House number:</label>
                            <div>
                                <Field type="text" name="houseNumber" className="order__form__inputs__row_input_width" style={{width: "125px"}}></Field>
                                {this.props.errors.houseNumber && this.props.touched.houseNumber ? <div className="validation">{this.props.errors.houseNumber}</div> : null}
                            </div>
                        </span>
                    </div>
                </div>
            </div>    
        )
    }
}

export default AddressForm

