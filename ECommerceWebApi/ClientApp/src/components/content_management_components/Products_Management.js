import React, { useEffect, useState } from 'react'
import {Link} from 'react-router-dom'
import '../../styles/cm_styles/cm__products__style.scss'
import Product_tr from './Product_tr'
import axios from 'axios';
import { toast } from 'react-toastify';
import { deleteProductRequest } from '../../services/api/ManagementRequests';

function Products_Management() {

    const [products,setProducts] = useState([]);
    const [error,setError] = useState(null);
    const [searchString,setSearchString] = useState("");

    const getProductsData = () => {
        axios.get("https://localhost:44333/api/products/getProducts")
        .then(function(response){
            console.log(response.data);
            setProducts(response.data)
        })
        .catch(function(error){
          setError(error)
        })
    }

    useEffect(() => {
        getProductsData(); 
    },[])

    const deleteProduct = async (productId,variationId) => {
        try{
            let response = await deleteProductRequest(productId,variationId);
            toast.success(response.data.message)
            getProductsData();
        } catch (err) {
            toast.error(error.response.data.message)
        }
    }

    const handleSearchChange = (e) => {
        setSearchString(e.target.value)
    }

    return (
        <div className="cm__products__container">
            <div className="cm__products__container__filter">
                <Link to="/manage/products/addProduct">Add product</Link>
                <label>Search : </label>
                <input onChange={handleSearchChange}></input>
            </div>
            <table>
                <thead>
                    <tr>
                    <th>ID</th>
                    <th>Image</th>
                    <th>Name</th>
                    <th>Category</th>
                    <th>SKU</th>
                    <th>Price</th>
                    <th>Modified at</th>
                    <th>Added at</th>
                    <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                {products
                .filter(product => product.name.toLowerCase().includes(searchString.toLowerCase()) || product.id.toString().includes(searchString) || product.sku.toLowerCase().includes(searchString.toLowerCase()) || product.categoryName.toLowerCase().includes(searchString.toLowerCase()))
                .map(product => (
                <Product_tr key={product.id} id={product.id} variationId={product.variationId} image={product.imageUrl} name={product.name} category={product.categoryName} sku={product.sku} price={product.price} delete={deleteProduct}/>
                ))}
                </tbody>
            </table>
        </div>
    )
}

export default Products_Management
