import React from 'react'
import {Formik,Form,Field, yupToFormErrors} from 'formik'
import * as Yup from 'yup'

function Edit_Category() {
    return (
        <div className="cm__category__create__container">
            <Formik>
                <Form>
                    <h2>Edit Category</h2>
                    <span>
                    <label className="cm__category__create__container__label">Name *</label>
                    <Field className="cm__category__create__container__input"></Field>
                    </span>
                    <button className="cm__category__create__container__create__button">Edit category</button>
                </Form>
            </Formik>
        </div>
    )
}

export default Edit_Category
