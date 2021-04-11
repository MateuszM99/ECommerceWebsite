import React, { useEffect, useState} from 'react';
import { Route, Redirect } from 'react-router-dom'
import jwt_decode from "jwt-decode";

const PrivateAdminRoute = ({ component: Component, ...rest }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(null);
  const [isAdmin,setIsAdmin] = useState(null);
  useEffect(() => {
    let user = JSON.parse(localStorage.getItem('userData'));
        if(user != null){
            let token = user.token;
            let tokenExpiration = jwt_decode(token).exp;
            let dateNow = new Date();

            if(tokenExpiration < dateNow.getTime()/1000){
              setIsAuthenticated(false)
              setIsAdmin(false);
            } else {
              setIsAuthenticated(true);
              console.log(user.userRoles);
              if(user.userRoles == "Admin"){
                  setIsAdmin(true);
              } else {
                setIsAdmin(false);
              }
            }
        } else {
          setIsAuthenticated(false)
          setIsAdmin(false);
        }
  },[isAuthenticated,isAdmin])

  if(isAuthenticated == null || isAdmin == null){
    return (
      <div>Waiting</div>
    )
  }
  return (
    <Route {...rest} render={props =>
      !isAuthenticated||!isAdmin  ? (
        <Redirect to='/login'/>
      ) : (
        <Component {...props} />
      )
    }
    />
  );
};

export default PrivateAdminRoute;