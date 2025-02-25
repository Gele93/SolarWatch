import React from 'react'

function Register({ setRegUsername, setRegEmail, setRegPassword }) {
    return (
        <div className='login-container'>
            <form>
                <div className='row'>
                    <label htmlFor='username'>Username</label>
                    <input autoComplete="off" id='username' type='text' onChange={(e) => setRegUsername(e.target.value)} />
                </div>
                <div className='row'>
                    <label htmlFor='email'>Email</label>
                    <input autoComplete="off" id='email' type='email' onChange={(e) => setRegEmail(e.target.value)} />
                </div>
                <div className='row'>
                    <label htmlFor='password'>Password</label>
                    <input autoComplete="off" id='password' type='password' onChange={(e) => setRegPassword(e.target.value)} />
                </div>
            </form>
        </div>
    )
}

export default Register