export default function PostData(type, userData) {
    let BaseURL = 'https://localhost:44333/api/';
    //let BaseURL = 'http://localhost/PHP-Slim-Restful/api/';

    return new Promise((resolve, reject) =>{       
        fetch(BaseURL+type, {
            method: 'POST',
            mode: 'cors',
            headers : {
            'Accept': 'application/json',
            'Content-Type' : 'application/json'
            },
            body: JSON.stringify(userData)
          })
          .then((response) => response.json())
          .then((res) => {
            resolve(res);
          })
          .catch((error) => {
            reject(error);
          });
      });
}
