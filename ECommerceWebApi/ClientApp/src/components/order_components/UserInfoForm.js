import { Field } from 'formik'
import React, { Component } from 'react'

export class UserInfoForm extends Component {

    constructor(props){
        super(props);

        this.state = {
            isLoggedIn : false,
            isChecked : false,
        }
    }

    componentDidMount(){
        if(localStorage.getItem('userData') != null){

            this.setState({
                isLoggedIn : true,
                isChecked : false
            })

            const userData = JSON.parse(localStorage.getItem('userData'));

            this.props.setFieldValue('name',userData.user.firstName);
            this.props.setFieldValue('lastName',userData.user.lastName);
            this.props.setFieldValue('email',userData.user.email);
            this.props.setFieldValue('phone',userData.user.phoneNumber);
        }
    }

    onCheckboxChange = () => {

        if(!this.state.isChecked){
            this.props.setFieldValue('name','');
            this.props.setFieldValue('lastName','');
            this.props.setFieldValue('email','');
            this.props.setFieldValue('phone','');
        } else {
            if(localStorage.getItem('userData') != null){
                let userData = JSON.parse(localStorage.getItem('userData'));
                console.log(userData);
                this.props.setFieldValue('name',userData.user.firstName);
                this.props.setFieldValue('lastName',userData.user.lastName);
                this.props.setFieldValue('email',userData.user.email);
                this.props.setFieldValue('phone',userData.user.phoneNumber);
            }
        }

        this.setState({
            isChecked : !this.state.isChecked
        })
    }


    render() {
        return (
            <div>
                <h3 className="order__form_header">User details</h3>
                <div className="order_user_check" style={{display : this.state.isLoggedIn ? 'flex' : 'none'}}>
                <p>Enter custom user details</p>
                <input type="checkbox" onChange={this.onCheckboxChange}></input>
                </div>
                <div className="order__form__inputs" style={{display : this.state.isChecked == this.state.isLoggedIn ? 'block' : 'none'}}> 
                    <div className="order__form__inputs__row">
                        <span>     
                            <label>Name:</label>
                            <div>
                                <Field type="text" name="name" className="order__form__inputs__row_input_width" style={{width: "250px"}}></Field>
                                {this.props.errors.name && this.props.touched.name ? <div className="validation">{this.props.errors.name}</div> : null}
                            </div>
                        </span>
                        <span>
                            <label>Last name:</label>
                            <div>
                                <Field type="text" name="lastName" className="order__form__inputs__row_input_width" style={{width: "250px"}}></Field>
                                {this.props.errors.lastName && this.props.touched.lastName ? <div className="validation">{this.props.errors.lastName}</div> : null}
                            </div>  
                        </span>  
                    </div>
                    <div className="order__form__inputs__row">   
                        <span>  
                            <label>Email:</label>
                            <div> 
                                <Field type="text" name="email" className="order__form__inputs__row_input_width" style={{width: "590px"}}></Field>       
                                {this.props.errors.email && this.props.touched.email ? <div className="validation">{this.props.errors.email}</div> : null}    
                            </div>
                        </span>
                    </div>
                    <div className="order__form__inputs__row">
                        <span>
                            <label>Phone number:</label>
                            <div>
                                <Field type="text" name="phone" className="order__form__inputs__row_input_width2"></Field>
                                {this.props.errors.phone && this.props.touched.phone ? <div className="validation">{this.props.errors.phone}</div> : null}
                            </div>
                        </span>  
                    </div>
                </div>
            </div>  
        )
    }
}

export default UserInfoForm
