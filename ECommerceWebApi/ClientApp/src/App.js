import React from 'react';
import logo from './logo.svg';
import './App.css';
import Header from './components/main_components/Header';
import Cart_View from './components/cart_components/Cart_View';
import Product from './components/product_components/Product';
import Product_View from './components/product_components/Product_View';
import Product_View_Header from './components/product_components/Product_View_Header';
import Product_View_Details from './components/product_components/Product_View_Details';
import Order_View from './components/order_components/Order_View';
import Profile_View from './components/profile_components/Profile_View';
import CheckLogin from './components/authentication_components/CheckLogin'
import Login_View from './components/authentication_components/Login_View';
import SignUp_View from './components/authentication_components/SignUp_View';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from "../node_modules/react-router-dom";
import axios from 'axios';
import { toast,ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'
import Login from './components/authentication_components/Login';
import SignUp from './components/authentication_components/SignUp';
import Content_Management_Main from './components/content_management_components/CM_Main'
import PrivateRoute from './PrivateRoute';
import PrivateAdminRoute from './PrivateAdminRoute';
import CartContextProvider from './contexts/CartContext';
import AccountEmailConfirm from './components/main_components/AccountEmailConfirm';
import OrderConfirm from './components/main_components/OrderConfirm';

function App() {

  if(localStorage.getItem('userData') != null){
  const userData = JSON.parse(localStorage.getItem('userData'));

    axios.interceptors.request.use(
      config => {
        config.headers.authorization = `Bearer ${userData.token} `;
        return config;
      },
      error => {
        return Promise.reject(error);
      }
    );
  }


  return (
    <Router>
      <CartContextProvider>
        <div className="App">
        <ToastContainer
          position="top-right"
          autoClose={2000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
        <Switch>
          <Route path="/login">
            <Login_View/>
          </Route>
          <Route path="/signup">
            <SignUp_View/>
          </Route>
          <Route path="/products" exact>
              <Header/>
              <Product_View_Header/>
              <Product_View/>
          </Route>   
          <Route path="/category/:category" exact>
              <Header/>
              <Product_View_Header/>
              <Product_View/>        
          </Route>
          <Route path="/category/:categoryId/:productId" exact>
              <Header/>
              <Product_View_Details/>
          </Route>
          <Route path="/order" exact>
            <Order_View/>
          </Route>
          <Route path="/order/confirm" exact>
            <Order_View/>
          </Route>
          <Route path="/checkLogin">
            <CheckLogin/>
          </Route>
          <PrivateRoute path="/profile" component={Profile_View}>
          </PrivateRoute>
          <PrivateAdminRoute path="/manage" component={Content_Management_Main}>
          </PrivateAdminRoute>
          <Route path="/accountConfirm" exact>
              <AccountEmailConfirm/>
          </Route>
          <Route path="/orderConfirm" exact>
              <OrderConfirm/>
          </Route>
          <Route path="/">
            <Header/>           
            <Product_View_Header/>
            <Product_View/>
          </Route>
        </Switch>
      </div>
      </CartContextProvider>
    </Router>
  );
}

export default App;
