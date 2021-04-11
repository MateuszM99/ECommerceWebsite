import React from 'react'
import CM_Nav_Tab from './CM_Nav_Tab'
import Products_Management from './Products_Management'
import Categories_Management from './Categories_Management'
import Options_Management from './Options_Management'
import Accounts_Management from './Accounts_Management'
import Orders_Management from './Orders_Management'
import Sales_Statistics from  './Sales_Statistics'
import CM_Header from './CM_Header'
import { Route } from 'react-router-dom'
import Create_Product from './Create_Product'
import Create_Category from './Create_Category'
import Create_Option from './Create_Option'
import Product_Edit from './Product_Edit'
import Edit_Category from './Edit_Category'
import Edit_Option from './Edit_Option'
import PrivateRoute from '../../'
import Product_Options_Stock from './Product_Options_Stock'
import Edit_Order from './Edit_Order'

function Content_Management_Main() {
    return (
        <div>
            <CM_Header/>
            <div className="profile__view__main__tab">
                <CM_Nav_Tab/>
                <Route path="/manage/products" exact>
                    <Products_Management/>
                </Route>
                <Route path="/manage/products/addProduct">
                    <Create_Product/>
                </Route>
                <Route path="/manage/products/editProduct/:id">
                    <Product_Edit/>
                </Route>
                <Route path="/manage/products/setOptionsStock/:id">
                    <Product_Options_Stock/>
                </Route>
                <Route path="/manage/categories" exact>
                    <Categories_Management/>
                </Route>
                <Route path="/manage/categories/addCategory">
                    <Create_Category/>
                </Route>
                <Route path="/manage/categories/editCategory/:id">
                    <Edit_Category/>
                </Route>
                <Route path="/manage/options" exact>
                    <Options_Management/>
                </Route>
                <Route path="/manage/options/addOption">
                    <Create_Option/>
                </Route>
                <Route path="/manage/options/editOption/:id">
                    <Edit_Option/>
                </Route>
                <Route path="/manage/accounts">
                    <Accounts_Management/>
                </Route>
                <Route path="/manage/orders" exact>
                    <Orders_Management/>
                </Route>
                <Route path="/manage/orders/editOrder/:id" exact>
                    <Edit_Order/>
                </Route>
                <Route path="/manage/sales_stats">
                    <Sales_Statistics/>
                </Route>
            </div>
        </div>
    )
}

export default Content_Management_Main
