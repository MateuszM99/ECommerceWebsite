import React, { Component } from 'react'
import {Formik,Form,Field, yupToFormErrors} from 'formik'
import axios from 'axios'
import '../../styles/profile_styles/profile__edit__style.scss'
import * as Yup from 'yup'
import {toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

export class Profile_Edit extends Component {

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
                <h2>Edit Profile</h2>
                <Formik
                    initialValues = {{
                        firstName : null,
                        lastName : null,
                        email : null,
                        phone : null,
                    }}
                    validationSchema = {Yup.object({
                        firstName : Yup.string()
                            .notRequired()
                            .nullable(true),
                        lastName : Yup.string()
                            .notRequired()
                            .nullable(true),
                        email : Yup.string()                          
                            .email('Email is invalid')
                            .notRequired()
                            .nullable(true),
                        phone : Yup.string()
                            .matches(phoneRegExp,'Phone number is invalid')
                            .notRequired()
                            .nullable(true),
                    })}

                    onSubmit = {(values,{setSubmitting, setStatus,resetForm}) => {
                        setTimeout(() => {
                            if(values != null){
                                console.log(values)
                               axios.post("https://localhost:44333/api/authenticate/editUser?username=" + user.userName,values)
                                .then(response =>{
                                    setSubmitting(false);
                                    resetForm();
                                    toast.success(response.data.response.message, {
                                        position: "top-right",
                                        autoClose: 2000,
                                        hideProgressBar: true,
                                        closeOnClick: true,
                                        pauseOnHover: true,
                                        draggable: true,
                                        progress: undefined,
                                        });
                                    this.state.userData.user = response.data.user                            
                                    localStorage.setItem('userData',JSON.stringify(this.state.userData));                                   
                                })
                                .catch(error => {
                                    setSubmitting(false);
                                    setStatus({
                                        errorMessage : error.response.data.message
                                    });
                                    console.log(error.response);
                                });  
                            }
                        },1000)
                    }}         
                >
                {({ errors, touched,isSubmitting,status }) => (
                    <Form>
                        <div>
                            <span>
                                <label className="profile__edit__label">First name</label>
                                <Field className="profile__edit__input_width1" name="firstName"></Field>
                                {errors.firstName && touched.firstName ? <div className="login-validation">{errors.firstName}</div> : null}
                            </span>
                            <span>
                                <label className="profile__edit__label">Last name</label>
                                <Field className="profile__edit__input_width1" name="lastName"></Field>
                                {errors.lastName && touched.lastName ? <div className="login-validation">{errors.lastName}</div> : null}
                            </span>
                        </div>
                        <label className="profile__edit__label">E-mail</label>
                        <Field className="profile__edit__input__width2" name="email"></Field>
                        {errors.email && touched.email ? <div className="login-validation">{errors.email}</div> : null}
                        <label className="profile__edit__label">Phone number</label>
                        <Field className="profile__edit__input__width2" name="phone"></Field>
                        {errors.phone && touched.phone ? <div className="login-validation">{errors.phone}</div> : null}
                        <button className="profile__edit__save__button" type="submit">{isSubmitting ? 'Saving ...' : 'Save changes'}</button>
                        {status && status.errorMessage ? (
                                <div className="edit-validation">{status.errorMessage}</div>
                            ) : null}
                    </Form>
                )}
                </Formik>
            </div>
            <div className="profile__info__container">
                <h2>Profile Info</h2>
                <div>
                    <span>
                        <label className="profile__info__label">First name</label>
                        <p className="profile__info__text">{user.firstName}</p>
                    </span>
                    <span>
                        <label className="profile__info__label">Last name</label>
                        <p className="profile__info__text">{user.lastName}</p>
                    </span>
                </div>
                <label className="profile__info__label">E-mail</label>
                <p className="profile__info__text2">{user.email}</p>
                <label className="profile__info__label">Phone number</label>
                <p className="profile__info__text2">{user.phoneNumber}</p>
            </div>
        </div>
        )
    }
}

export default Profile_Edit
