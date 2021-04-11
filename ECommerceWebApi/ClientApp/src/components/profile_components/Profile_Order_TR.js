import React from 'react'

function Profile_Order_TR(props) {
    return (
        <div>
            <tr>
                <td>{props.id}</td>
                <td>{props.status}</td>
                <td>{props.price}</td>
                <td>{props.modifiedAt}</td>
                <td>{props.addedAt}</td>
            </tr>
        </div>
    )
}

export default Profile_Order_TR
