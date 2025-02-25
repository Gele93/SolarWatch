import React, { useEffect, useState } from 'react'
import Login from '../components/Landing/Login'
import Register from '../components/Landing/Register'
import PanelButtons from '../components/Landing/PanelButtons'
import { fetchLogin, fetchRegister } from '../scripts/requests'
import ActionButton from '../components/ActionButton'
import { useNavigate } from "react-router-dom"

function Landing({ userProps }) {

    const { user, setUser, userRole, setUserRole } = userProps

    const navigate = useNavigate();

    const [isLogin, setIsLogin] = useState(true)
    const [message, setMessage] = useState("")
    const [isError, setIsError] = useState(true)

    const [loginEmail, setLoginEmail] = useState()
    const [loginPassword, setLoginPassword] = useState()
    const [regUsername, setRegUsername] = useState()
    const [regEmail, setRegEmail] = useState()
    const [regPassword, setRegPassword] = useState()


    const handleLogin = async () => {
        const loginData = { "Email": loginEmail, "Password": loginPassword }
        const user = await fetchLogin(loginData)
        if (user === null) {
            setIsError(true)
            return setMessage("User not found")
        }
        const userData = { "username": user.username, "email": user.email }
        setUser(userData)
        localStorage.setItem("user", JSON.stringify(userData))
        localStorage.setItem("userToken", user.token)
        navigate("/sun")

    }
    const handleRegister = async () => {
        const regData = { "Username": regUsername, "Email": regEmail, "Password": regPassword }
        const user = await fetchRegister(regData)
        if (user === null) {
            setIsError(true)
            return setMessage("Incorrect user informations")
        }
        setIsError(false)
        setMessage("Successful registration!")
        return setIsLogin(true)
    }

    return (
        <div className={`landing-page ${isLogin ? "left" : "right"}`}>
            <div className='header'>
                <p className={`title ${isLogin ? "left" : "right"}`}>Solar Watch</p>
            </div>
            <div className='user-container'>
                <div className='user-panel'>
                    <PanelButtons setIsLogin={setIsLogin} isLogin={isLogin} />
                    {isLogin ?
                        <>
                            <Login setLoginEmail={setLoginEmail} setLoginPassword={setLoginPassword} />
                            <ActionButton buttonText={"Login"} action={handleLogin} />
                        </>
                        :
                        <>
                            <Register setRegUsername={setRegUsername} setRegEmail={setRegEmail} setRegPassword={setRegPassword} />
                            <ActionButton buttonText={"Register"} action={handleRegister} />
                        </>
                    }
                    <div className={`message ${isError && "error"}`}>{message}</div>
                </div>
            </div>
        </div>
    )
}

export default Landing