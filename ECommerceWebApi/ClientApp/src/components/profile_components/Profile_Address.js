import React, { Component } from 'react'
import {Formik,Form,Field} from 'formik'
import CountrySelectList from '../CountrySelectList'
import * as Yup from 'yup'
import '../../styles/profile_styles/profile__edit__style.scss'
import axios from 'axios'
import {toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'

if(localStorage.getItem('userData') != null){
    var user = JSON.parse(localStorage.getItem('userData'));
}


export class Profile_Address extends Component {

    constructor(){
        super();

        this.state = {
            userData : null
        }
    }

    componentDidMount(){
        if(localStorage.getItem('userData') != null){
            let userData = JSON.parse(localStorage.getItem('userData'));
            this.setState({userData})
            console.log(userData)
        }
        
    }

    render() {
        const user = this.state.userData ? this.state.userData.user : null;
        if(user == null){
        return(
            <div>Loading</div>
        ) }
        return (
        <div className="profile__edit__main__container">
            <div className="profile__edit__container">
                <h2>Edit Address</h2>
                <Formik
                    initialValues = {{
                        postCode : '',
                        country : '',
                        city : '',
                        street : '',
                        houseNumber : '',
                    }}
                    validationSchema = {Yup.object({
                        postCode : Yup.string()
                            
                    })}

                    onSubmit = {(values,{setSubmitting, setStatus,resetForm}) => {
                        setTimeout(() => {
                            if(values != null){
                               axios.post("https://localhost:44333/api/authenticate/editAddress?username=" + user.user.userName,values)
                                .then(function(response){
                                    toast.success(response.data.message, {
                                        position: "top-right",
                                        autoClose: 2000,
                                        hideProgressBar: true,
                                        closeOnClick: true,
                                        pauseOnHover: true,
                                        draggable: true,
                                        progress: undefined,
                                        });
                                    setSubmitting(false);
                                    resetForm();
                                })                               
                                .catch(error => {
                                    setSubmitting(false);
                                    setStatus({
                                        errorMessage : error.response.data.message
                                    });
                                });  
                            }
                        },1000)
                    }}   
                >
                {({ errors, touched,values,isSubmitting,status }) => (
                    <Form>
                        <div>
                            <span>
                                <label className="profile__edit__label">Post Code</label>
                                <Field className="profile__edit__input_width1" name="postCode"></Field>
                                {errors.postCode && touched.postCode ? <div className="validation">{errors.postCode}</div> : null}
                            </span>
                            <span>
                                <label className="profile__edit__label">Country</label>
                                <CountrySelectList class={"profile__edit__input_width1"} name="country" />
                                {errors.country && touched.country ? <div className="validation">{errors.country}</div> : null}
                            </span>
                        </div>
                        <label className="profile__edit__label">City</label>
                        <Field className="profile__edit__input__width2" name="city"></Field>
                        {errors.city && touched.city ? <div className="validation">{errors.city}</div> : null}
                        <label className="profile__edit__label">Street</label>
                        <Field className="profile__edit__input__width2" name="street"></Field>
                        {errors.street && touched.street ? <div className="validation">{errors.street}</div> : null}
                        <label className="profile__edit__label">House number</label>
                        <Field className="profile__edit__input__width0" name="houseNumber"></Field>
                        {errors.houseNumber && touched.houseNumber ? <div className="validation">{errors.houseNumber}</div> : null}
                        <button className="profile__edit__save__button" type="submit">{isSubmitting ? 'Saving ...' : 'Save changes'}</button>
                        {status && status.errorMessage ? (
                                <div className="edit-validation">{status.errorMessage}</div>
                            ) : null}
                    </Form>
                )}
                </Formik>               
            </div>
            <div className="profile__info__container">
            <h2>Address Info</h2>
            <div>
                <span>
                    <label className="profile__info__label">Post code</label>
                    <p className="profile__info__text">{user.address ? user.address.postCode : null}</p>
                </span>
                <span>
                    <label className="profile__info__label">Country</label>
                    <p className="profile__info__text">{user.address ? user.address.country : null}</p>
                </span>
            </div>
            <label className="profile__info__label">City</label>
            <p className="profile__info__text2">{user.address ? user.address.city : null}</p>
            <label className="profile__info__label">Street</label>
            <p className="profile__info__text2">{user.address ? user.address.street : null}</p>
            <label className="profile__info__label">House number</label>
            <p className="profile__info__text0">{user.address ? user.address.houseNumber : null}</p>
        </div>
    </div>
        )
    }
}

export default Profile_Address
