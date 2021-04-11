import React,{ useEffect, useState } from 'react'
import '../../styles/cm_styles/cm__options__style.scss'
import Option_tr from './Option_tr'
import axios from 'axios';
import {Link} from 'react-router-dom'
import { deleteOptionRequest } from '../../services/api/ManagementRequests';
import { toast } from 'react-toastify'


function Options_Management() {

    const [options,setOptions] = useState([]);
    const [error,setError] = useState(null);
    const [searchString,setSearchString] = useState("");

    const getAllOptions = () => {
        axios.get("https://localhost:44333/api/products/getOptions")
        .then(function(response){
            console.log(response.data);
            setOptions(response.data)
        })
        .catch(function(error){
          setError(error)
        })
    }

    useEffect(() => {
        getAllOptions();  
    },[])

    const deleteOption = async (optionId) => {
        try{
            let response = await deleteOptionRequest(optionId);
            toast.success(response.data.message)
            getAllOptions();
        } catch (err) {
            toast.error('Error')
        }
    }

    const handleSearchChange = (e) => {
        setSearchString(e.target.value)
    }

    return (
        <div className="cm__options__container">
            <div className="cm__options__container__filter">
                <Link to="/manage/options/addOption">Add option</Link>
                <label>Search : </label>
                <input onChange={handleSearchChange}></input>
            </div>
            <table>
                <thead>
                    <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                 {options
                 .filter(option => option.name.toLowerCase().includes(searchString.toLowerCase()) || option.id.toString().includes(searchString))
                 .map(option => (
                     <Option_tr key={option.id} id={option.id} name={option.name} delete={deleteOption}/>
                 ))}
                </tbody>
            </table>
        </div>
    )
}

export default Options_Management
