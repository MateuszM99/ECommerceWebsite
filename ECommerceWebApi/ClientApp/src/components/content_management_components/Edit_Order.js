import React from 'react'
import { Field, Formik,Form } from 'formik'
import * as Yup from 'yup';
import CountrySelectList from '../CountrySelectList'
import { toast } from 'react-toastify'
import { editOrderRequest } from '../../services/api/ManagementRequests';

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

export default function Edit_Order(){
    
    return (
        <Formik
            initialValues={{
                email : '',
                name : '',
                lastName :'',
                street : '',
                houseNumber : '',
                postCode : '',
                city : '',
                country : '',
                phone : '',
                status : '',
            }}

            validationSchema = {
                Yup.object().shape({
                    name : Yup.string()
                        .min(2,'Must be at least 2 characters'),
                    email : Yup.string()
                        .email('Invalid email'),
                    phone : Yup.string()
                            .matches(phoneRegExp, 'Phone number is not valid'),
                })
            }

            
            onSubmit = {(values,{setSubmitting,resetForm}) => {
                setTimeout(async () => {
                    if(values != null){
                        console.log(values)
                        try{
                            await editOrderRequest(values);
                            toast.success('Succesfully edited')
                            setSubmitting(false);
                            resetForm();
                        } catch(error) {
                            toast.error('Something went wrong');
                        }  
                    }
                },1000)
            }}          
        >
        {({ errors, touched,isSubmitting}) => (
            <Form>
                <div className="edit__order__container">    
                    <h3 className="order__form_header">Edit Order</h3>
                    <div className="order__form__inputs">
                        <h3 className="order__form__sub__header">User info</h3>
                        <div className="order__form__inputs__row">
                            <span>     
                                <label>Name:</label>
                                <div>
                                    <Field type="text" name="name" className="order__form__inputs__row_input_width" style={{width: "250px"}}></Field>                                
                                    {errors.name && touched.name ? <div className="validation">{errors.name}</div> : null}
                                </div>
                            </span>
                            <span>
                                <label>Last name:</label>
                                <div>
                                    <Field type="text" name="lastName" className="order__form__inputs__row_input_width" style={{width: "250px"}}></Field>                               
                                </div>  
                            </span>  
                        </div>
                        <div className="order__form__inputs__row">   
                            <span>  
                                <label>Email:</label>
                                <div> 
                                    <Field type="text" name="email" className="order__form__inputs__row_input_width" style={{width: "590px"}}></Field>                                        
                                    {errors.email && touched.email ? <div className="validation">{errors.email}</div> : null}
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Phone number:</label>
                                <div>
                                    <Field type="text" name="phone" className="order__form__inputs__row_input_width2"></Field>                              
                                    {errors.phone && touched.phone ? <div className="validation">{errors.phone}</div> : null}
                                </div>
                            </span>  
                        </div>
                        <h3 className="order__form__sub__header">Address info</h3>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Country:</label>
                                <div>
                                    <CountrySelectList name="country" class={"order__form__inputs__row_input_width"} style={{width: "400px"}}/>                                      
                                </div>
                            </span>
                            <span>
                                <label>Post-Code:</label>
                                <div>
                                    <Field type="text" name="postCode" className="order__form__inputs__row_input_width" style={{width: "125px"}}></Field>                                        
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>City:</label>
                                <div>
                                    <Field type="text" name="city" className="order__form__inputs__row_input_width2"></Field>                                        
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Street:</label>
                                <div>
                                    <Field type="text" name="street" className="order__form__inputs__row_input_width" style={{width: "400px"}}></Field>                                      
                                </div>
                            </span>
                            <span>
                                <label>House number:</label>
                                <div>
                                    <Field type="text" name="houseNumber" className="order__form__inputs__row_input_width" style={{width: "125px"}}></Field>                                      
                                </div>
                            </span>
                        </div>                
                        <h3 className="order__form__sub__header">Order info</h3>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Status:</label>
                                <div>
                                    <Field type="text" as="select" name="status" className="order__form__inputs__row_input_width" style={{width: "300px"}}>
                                        <option>Awaiting Payment</option>
                                        <option>Processing In Progress</option>
                                        <option>Payment Accepted</option>
                                        <option>Shipped</option>
                                        <option>Refunded</option>
                                        <option>Canceled</option>
                                        <option>Delivered</option>
                                        <option>On Backorder</option>                                       
                                    </Field>                                      
                                </div>
                            </span>
                        </div> 
                    </div>
                    <button className="cm__orders__container__button">Edit</button>
                </div>
            </Form>
        )}
        </Formik>
    )

}