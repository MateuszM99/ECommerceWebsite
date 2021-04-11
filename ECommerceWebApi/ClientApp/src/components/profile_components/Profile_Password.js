import React, { Component } from 'react'
import {Formik,Form,Field} from 'formik'
import CountrySelectList from '../CountrySelectList'
import * as Yup from 'yup'
import '../../styles/profile_styles/profile__edit__style.scss'
import axios from 'axios'
import {toast} from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'

function Profile_Password() {
    return (
        <div className="profile__edit__main__container">
            <div className="profile__edit__container">
                <h2>Change password</h2>
                <Formik
                    initialValues = {{
                        oldPassword : '',
                        newPassword : '',
                        newPasswordConfirm : '',           
                    }}
                    validationSchema = {Yup.object({  
                        oldPassword : Yup.string().required('Old password is required'),                     
                        newPassword : Yup.string()
                        .required('New password is required')
                        .matches(
                            /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
                            "Must contain 8 characters, one uppercase, one lowercase, one number and one special case character"
                          ),
                        newPasswordConfirm: Yup.string().required('Password confirm is required')
                        .oneOf([Yup.ref("password"), null], "Passwords must match")  
                        })}

                    onSubmit = {(values,{setSubmitting, setStatus,resetForm}) => {
                        setTimeout(() => {
                            if(values != null){
                              axios.post("https://localhost:44333/api/authenticate/editPassword,values")
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
                                })
                            }
                        },1000)
                    }}   
                >
                {({ errors, touched,values,isSubmitting,status }) => (
                    <Form>
                        <label className="profile__edit__label">Old password</label>
                        <Field className="profile__edit__input__width2" name="oldPassword"></Field>
                        {errors.oldPassword && touched.oldPassword ? <div className="validation">{errors.oldPassword}</div> : null}
                        <label className="profile__edit__label">New password</label>
                        <Field className="profile__edit__input__width2" name="newPassword"></Field>
                        {errors.newPassword && touched.newPassword ? <div className="validation">{errors.newPassword}</div> : null}
                        <label className="profile__edit__label">New password confirm</label>
                        <Field className="profile__edit__input__width2" name="newPasswordConfirm"></Field>
                        {errors.newPasswordConfirm && touched.newPasswordConfirm ? <div className="validation">{errors.newPasswordConfirm}</div> : null}
                        <button className="profile__edit__save__button" type="submit">{isSubmitting ? 'Saving ...' : 'Save changes'}</button>
                        {status && status.errorMessage ? (
                                <div className="edit-validation">{status.errorMessage}</div>
                            ) : null}
                    </Form>
                )}
                </Formik>               
            </div>
        </div>   
    )
}

export default Profile_Password
