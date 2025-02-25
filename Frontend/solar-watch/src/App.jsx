import React from 'react'
import { useState, useEffect } from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Landing from './pages/Landing';
import Sun from './pages/Sun';
import EditData from './pages/EditData';


function App() {

  const [user, setUser] = useState({})
  const [userRole, setUserRole] = useState("")

  const userProps = {user, setUser, userRole, setUserRole}

  useEffect(() => {
    if (localStorage.getItem("user")) {
      setUser(JSON.parse(localStorage.getItem("user")))
    }
  }, [])

  return (
    <Router>
      <Routes>
        <Route path='' element={<Landing userProps={userProps} />} />
        <Route path='/sun' element={<Sun userProps={userProps} />} />
        <Route path='/edit-data' element={<EditData userProps={userProps} />} />
      </Routes>
    </Router>
  )
}

export default App
