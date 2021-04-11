import React from 'react'
import {Formik,Form,Field, yupToFormErrors} from 'formik'
import * as Yup from 'yup'
import { addOptionRequest } from '../../services/api/ManagementRequests';
import { toast } from 'react-toastify'

function Create_Option() {
    return (
        <div className="cm__options__create__container">
            <Formik
            initialValues = {{
                name : '',
            }}
            validationSchema = {Yup.object({
                name : Yup.string()
                    .required('Name is required'),  
            })}

            onSubmit = {(values,{setSubmitting, setStatus,resetForm}) => {
                setTimeout(async () => {
                    if(values != null){
                        try{
                            let response = await addOptionRequest(values);
                            toast.success(response.data.message)
                            setSubmitting(false);
                            resetForm();
                        } catch(error) {
                            setSubmitting(false);
                            setStatus({
                                errorMessage : error.response.data
                            });
                            toast.error(error.response.data.message);
                        }  
                    }
                },1000)
            }}  
            >
            {({ errors, touched,isSubmitting,status,setFieldValue }) => (
                <Form>
                    <h2>Create Option</h2>
                    <span>
                    <label className="cm__options__create__container__label">Name *</label>
                    <Field className="cm__options__create__container__input" name="name"></Field>
                    </span>
                    {errors.name && touched.name ? <div className="cm__products__create__container__validation">{errors.name}</div> : null}
                    <button className="cm__options__create__container__create__button">Create option</button>
                </Form>
            )}
            </Formik>
        </div>
    )
}

export default Create_Option
