import React, { Component } from 'react'
import '../../styles/order_styles/order__style.scss'
import Products_List from './Products_List'
import {Formik,Form, yupToFormErrors,Field} from 'formik'
import * as Yup from 'yup'
import { withRouter,Link,useHistory,Redirect } from "react-router-dom";
import axios from 'axios'
import { makeStyles } from '@material-ui/core/styles';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Button from '@material-ui/core/Button';
import validationSchema from './validationSchema'
import UserInfoForm from './UserInfoForm'
import AddressForm from './AddressForm'
import PaymentDeliveryForm from './PaymentDeliveryForm'
import ReviewOrder from './ReviewOrder'
import { withStyles } from '@material-ui/core/styles';
import StepConnector from '@material-ui/core/StepConnector';
import { StepIcon } from '@material-ui/core'
import Check from '@material-ui/icons/Check';
import clsx from 'clsx';
import { CartContext } from '../../contexts/CartContext'
import { toast } from 'react-toastify';

const QontoConnector = withStyles({
    alternativeLabel: {
      top: 10,
      left: 'calc(-50% + 16px)',
      right: 'calc(50% + 16px)',
    },
    active: {
      '& $line': {
        borderColor: 'rgb(67, 68, 87)',
      },
    },
    completed: {
      '& $line': {
        borderColor: 'rgb(67, 68, 87)',
      },
    },
    line: {
      borderColor: 'lightgray',
      borderTopWidth: 3,
      borderRadius: 1,
    },
  })(StepConnector);


  const useQontoStepIconStyles = makeStyles({
    root: {
      color: 'rgb(67, 68, 87)',
      display: 'flex',
      height: 22,
      alignItems: 'center',
    },
    active: {
      color: 'rgb(67, 68, 87)',
    },
    circle: {
      width: 8,
      height: 8,
      borderRadius: '50%',
      backgroundColor: 'currentColor',
    },
    completed: {
      color: 'rgb(67, 68, 87)',
      zIndex: 1,
      fontSize: 18,
    },
  });

  function QontoStepIcon(props) {
    const classes = useQontoStepIconStyles();
    const { active, completed } = props;
  
    return (
      <div
        className={clsx(classes.root, {
          [classes.active]: active,
        })}
      >
        {completed ? <Check className={classes.completed} /> : <div className={classes.circle} />}
      </div>
    );
  }


const styles = {
    root : {
    }
}

export class Order_View extends Component {

    static contextType = CartContext;

    constructor(props) {
        super(props);

        this.state = {
            activeStep : 0,
            steps : ['User Details', 'Address Details', 'Select payment and delivery','Review Order'],
            delivery : 0,
            payment : null
        }
    }

    classes = this.props.classes;

    handleDeliveryChange = (price) => {
        this.setState({delivery : price});
    } 

    handlePaymentChange = (e) => {
      console.log(e)
        this.setState({payment : e.target.value});
    } 

    renderStepContent = (step,errors,touched,setFieldValue) => {
        switch(step){
        case 0:
            return (
                <UserInfoForm errors={errors} touched={touched} setFieldValue = {setFieldValue}/>
            )
        case 1: 
            return (
                <AddressForm errors={errors} touched={touched} setFieldValue = {setFieldValue}/>
            )  
        case 2:
            return(
                <PaymentDeliveryForm 
                delivery={this.state.delivery} 
                payment={this.state.payment} 
                handleDeliveryChange={this.handleDeliveryChange} 
                handlePaymentChange={this.handlePaymentChange} 
                errors={errors} touched={touched}
                setFieldValue = {setFieldValue}
                />
            )
        case 3:
            return(
                <ReviewOrder/>
            ) 
        default :
        }
    }

    submitForm = (values, actions) => {
        setTimeout(() => {      
            if(values != null){               
                let cartId = JSON.parse(localStorage.getItem("cartData")).cartId;
                axios.post("https://localhost:44333/api/order/createOrder",
                          {
                            clientEmail : values.email,
                            clientName : values.name,
                            clientSurname : values.lastName,
                            clientPhone : values.phone,
                            address : {
                              street : values.street,
                              houseNumber : values.houseNumber,
                              postCode : values.postCode,
                              city : values.city,
                              country : values.country
                            },                        
                            deliveryMethodId : values.delivery_method,
                            paymentMethodId : values.payment_method,
                            cartId : cartId
                          })
                    .then(response => {                     
                        toast.success('Order placed succesfully, go to your email and confirm it')   
                        this.props.history.push({
                          pathname : "/"
                        })                      
                    })
                    .catch(error => {                     
                      toast.error('Something went wrong')
                      this.setState({activeStep : 0})
                });  
            }                             
        },1000)
        actions.setSubmitting(false);
      }

    handleSumbit = (values,actions) => {
        if (this.state.activeStep === this.state.steps.length - 1) {
            this.submitForm(values, actions);
          } else {
        this.setState({activeStep : this.state.activeStep + 1})
        actions.setSubmitting(false);
        }
    }

    handleBack = () => {
        this.setState({activeStep : this.state.activeStep - 1})
    }



    render() {
        const {activeStep} = this.state;
        const {steps} = this.state;
        const isLastStep = activeStep === steps.length - 1;
        const currentValidationSchema = validationSchema[activeStep];
        if(this.context.cartCount == 0){
          return (
            <div style={{display : 'flex', width : '100%', height : '100vh', justifyContent : 'center', alignItems : 'center'}}>
              Your cart is empty
            </div>
          )
        } else {
          return (
                  <div className="order__container">
                      <div className="order__header__container">
                          <Link to="/"><button className="back__button">Back to shop</button></Link>
                      </div>
                      <div className="order__main__container">
                          <div className="order__main__container__forms">
                              <div className={this.classes.root}>
                              <Stepper activeStep={activeStep} alternativeLabel connector={<QontoConnector/>}>
                                  {steps.map((label) => (
                                  <Step key={label}>
                                      <StepLabel StepIconComponent={QontoStepIcon}>{label}</StepLabel>
                                  </Step>
                                  ))}
                              </Stepper>
                          </div>
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
                                      delivery_method : '',
                                      payment_method : '',
                                  }}
                                  validationSchema={currentValidationSchema}                               
                                  onSubmit={this.handleSumbit}                    
                              >
                        {({ errors, touched,isSubmitting,setFieldValue}) => (
                            <Form>
                            <div className="form">
                                {this.renderStepContent(activeStep,errors,touched,setFieldValue)}
                                <div className="form_buttons">
                                {activeStep !== 0 && (
                                      <button className="form__button__step" onClick={this.handleBack} type="button">Previous</button>
                                  )}  
                                  <button className="form__button__step" type="submit">{isLastStep ? 'Place order' : 'Next'}</button>
                                </div>
                            </div>
                            </Form>
                        )}
                        </Formik>
                      </div>   
                      <Products_List delivery={this.state.delivery}/>
                  </div>
              </div>
          )
        }
    }
}

export default withStyles(styles)(withRouter(Order_View));
