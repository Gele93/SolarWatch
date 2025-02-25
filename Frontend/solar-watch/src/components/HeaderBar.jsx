import React from 'react'
import Profile from './Profile'
import { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom"
import { jwtDecode } from "jwt-decode";
import ActionButton from './ActionButton';

function HeaderBar({ userProps, headerText }) {

  const navigate = useNavigate()

  const { user, setUser, userRole, setUserRole } = userProps
  const [isEditPage, setIsEditPage] = useState(false)


  useEffect(() => {
    if (window.location.pathname.split("/").includes("edit-data")) {
      setIsEditPage(true)
    }
  }, [])

  useEffect(() => {
    if (localStorage.getItem("userToken")) {
      const decodedToken = jwtDecode(localStorage.getItem("userToken"))
      for (const key in decodedToken) {
        if (key.split("/").includes("role")) {
          setUserRole(decodedToken[key])
        }
      }
    }
  }, [])

  const handleLogut = () => {
    localStorage.setItem("userToken", "")
    localStorage.setItem("user", "")
    setUser({})
    return navigate("/")
  }

  console.log(userRole)

  return (
    <div className='header-bar'>
      <div className='left'>
        {user &&
          <Profile user={user} />
        }
      </div>
      <div className='mid'>
        <p className="title">{headerText}</p>
      </div>
      <div className='right yellow'>
        {userRole === "Admin" && (
          isEditPage ?
            <ActionButton buttonText="Solar watch" action={() => navigate("/sun")} />
            :
            <ActionButton buttonText="Edit data" action={() => navigate("/edit-data")} />
        )
        }
        <ActionButton buttonText="Logout" action={handleLogut} />
      </div>
    </div>
  )
}

export default HeaderBar