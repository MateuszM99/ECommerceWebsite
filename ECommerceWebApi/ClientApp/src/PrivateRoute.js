import React, { useEffect, useState} from 'react';
import { Route, Redirect } from 'react-router-dom'
import jwt_decode from "jwt-decode";

const PrivateRoute = ({ component: Component, ...rest }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(null);
  useEffect(() => {
    let user = JSON.parse(localStorage.getItem('userData'));
        if(user != null){
            let token = user.token;
            let tokenExpiration = jwt_decode(token).exp;
            let dateNow = new Date();

            if(tokenExpiration < dateNow.getTime()/1000){
              setIsAuthenticated(false)
            } else {
              setIsAuthenticated(true);
            }
        } else {
          setIsAuthenticated(false)
        }
  },[isAuthenticated])

  if(isAuthenticated == null){
    return (
      <div>Waiting</div>
    )
  }
  return (
    <Route {...rest} render={props =>
      !isAuthenticated  ? (
        <Redirect to='/login'/>
      ) : (
        <Component {...props} />
      )
    }
    />
  );
};

export default PrivateRoute;