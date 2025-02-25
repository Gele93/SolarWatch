import React from 'react'

function Login({ setLoginEmail, setLoginPassword }) {
    return (
        <div className='login-container'>
            <form>
                <div className='row'>
                    <label htmlFor='email'>Email</label>
                    <input autoComplete="off" id='email' type='email' onChange={(e) => setLoginEmail(e.target.value)} />
                </div>
                <div className='row'>
                    <label htmlFor='password'>Password</label>
                    <input autoComplete="off" id='password' type='password' onChange={(e) => setLoginPassword(e.target.value)} />
                </div>
            </form>
        </div>
    )
}

export default Login