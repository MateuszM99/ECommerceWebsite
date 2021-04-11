import * as Yup from 'yup';

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

 export default [
    Yup.object().shape({
        name : Yup.string()
            .min(2,'Must be at least 2 characters')
            .required('Name is required'),
        email : Yup.string()
            .email('Invalid email')
            .required('Email is required'),
        lastName : Yup.string()
                .required('Last name is required'),
        phone : Yup.string()
                .matches(phoneRegExp, 'Phone number is not valid')
                .required('Phone number is required'),
    }),
    Yup.object().shape({
        street : Yup.string()
            .required('Street is required'),
        houseNumber : Yup.string()
            .required('House number is required'),
        postCode : Yup.string()
            .required('Post code is required'),
        city : Yup.string()
            .required('City is required'),
        country : Yup.string()
            .required('You must choose country'),
    }),
    Yup.object().shape({
        delivery_method : Yup.string()
            .required('You must choose delivery method'),
        payment_method : Yup.string()
            .required('You must choose a payment method'),
    })
]