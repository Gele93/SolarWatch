import React, { useEffect, useState } from 'react'
import ActionButton from '../components/ActionButton'
import SunMoveForm from '../components/Sun/SunMoveForm'
import SunAnimationContainer from '../containers/SunAnimationContainer'
import { fetchSunData } from '../scripts/requests'
import { useNavigate } from "react-router-dom"
import SunMovement from '../components/Sun/SunMovement'
import { fetchCityNames } from '../scripts/requests'
import HeaderBar from '../components/HeaderBar.jsx'



function Sun({userProps}) {

  const { user, setUser, userRole, setUserRole } = userProps

  const navigate = useNavigate()

  const [city, setCity] = useState("")
  const [date, setDate] = useState(new Date())
  const [sunmovement, setSunmovement] = useState({})
  const [isLoading, setIsLoading] = useState(false)
  const [cityNames, setCityNames] = useState([])
  const [citySuggestions, setCitySuggestions] = useState([])

  useEffect(() => {
    if (!localStorage.getItem("userToken")) {
      navigate("/")
    }
  }, [])


  const getSunData = async () => {
    setIsLoading(true)
    const sunData = await fetchSunData(city, date, localStorage.getItem("userToken"))
    if (!sunData) setSunmovement("unknown")
    setIsLoading(false)
    return setSunmovement(sunData)
  }


  const getCityNames = async (filterName) => {
    const cities = await fetchCityNames(filterName, localStorage.getItem("userToken"))
    return setCityNames(cities)
  }
  const filterCityNames = (filterName) => {
    let isDelete = city.length > filterName.length
    let updatedSuggestions = citySuggestions.length === 0 ? cityNames : isDelete ? cityNames : citySuggestions
    updatedSuggestions = updatedSuggestions.filter(c => c.name.toLowerCase().includes(filterName.toLowerCase()))
    return setCitySuggestions(updatedSuggestions)
  }

  const handleCityChange = (value) => {
    if (value.length < 3) {
      getCityNames(value)
      setCitySuggestions([])
    } else {
      filterCityNames(value)
    }
    setCity(value)
  }

  return (
    <SunAnimationContainer>
      <HeaderBar userProps={userProps} headerText={"Solar Watch"} />
      <div className='sun-move-form-container'>
        <SunMoveForm handleCityChange={handleCityChange} setDate={setDate} cityNames={cityNames} citySuggestions={citySuggestions} />
        <ActionButton buttonText={"Check"} action={getSunData} />
      </div>
      <div className='sunmovement-container'>
        {isLoading ?
          <div className='loader'></div>
          :
          sunmovement &&
          <SunMovement sunmovement={sunmovement} />
        }
      </div>
    </SunAnimationContainer>
  )
}

export default Sun