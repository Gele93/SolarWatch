import React from 'react'

function Profile({ user }) {
    return (
        <div className='profile'>{user.username}</div>
    )
}

export default Profile