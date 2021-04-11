import React, { Component } from 'react'
import { useFormikContext } from 'formik';

export default function ReviewOrder(){
    
    
        const { values: formValues } = useFormikContext();
        return (
            <div>    
                <h3 className="order__form_header">Review Order</h3>
                <div className="order__form__inputs">
                    <div className="order__form__inputs__row">
                            <span>     
                                <label>Name:</label>
                                <div>
                                    <p className="order__form__inputs__row_input_width" style={{width: "250px"}}>{formValues.name}</p>      
                                </div>
                            </span>
                            <span>
                                <label>Last name:</label>
                                <div>
                                    <p className="order__form__inputs__row_input_width" style={{width: "250px"}}>{formValues.lastName}</p>
                                </div>  
                            </span>  
                        </div>
                        <div className="order__form__inputs__row">   
                            <span>  
                                <label>Email:</label>
                                <div> 
                                    <p type="text" name="email" className="order__form__inputs__row_input_width" style={{width: "590px"}}>{formValues.email}</p>       
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Country:</label>
                                <div>
                                    <p class={"order__form__inputs__row_input_width"} style={{width: "400px"}}>{formValues.country}</p>
                                </div>
                            </span>
                            <span>
                                <label>Post-Code:</label>
                                <div>
                                    <p type="text" name="postCode" className="order__form__inputs__row_input_width" style={{width: "125px"}}>{formValues.postCode}</p>
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>City:</label>
                                <div>
                                    <p type="text" name="city" className="order__form__inputs__row_input_width2">{formValues.city}</p>
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Street:</label>
                                <div>
                                    <p type="text" name="street" className="order__form__inputs__row_input_width" style={{width: "400px"}}>{formValues.street}</p>
                                </div>
                            </span>
                            <span>
                                <label>House number:</label>
                                <div>
                                    <p type="text" name="houseNumber" className="order__form__inputs__row_input_width" style={{width: "125px"}}>{formValues.houseNumber}</p>
                                </div>
                            </span>
                        </div>
                        <div className="order__form__inputs__row">
                            <span>
                                <label>Phone number:</label>
                                <div>
                                    <p type="text" name="phone" className="order__form__inputs__row_input_width2">{formValues.phone}</p>
                                </div>
                            </span>  
                        </div>
                </div>
            </div>
        )
    
}

