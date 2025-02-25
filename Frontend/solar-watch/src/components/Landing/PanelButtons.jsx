import React from 'react'

function PanelButtons({ setIsLogin, isLogin}) {
        return (
        <div className='login-register'>
            <button className={`login ${isLogin ? "active" : ""}`} onClick={() => setIsLogin(true)}>Login</button>
            <button className={`reg ${isLogin ? "" : "active"}`}onClick={() => setIsLogin(false)}>Register</button>
        </div>
    )
}

export default PanelButtons