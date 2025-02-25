import React from 'react'
import SunMoveRow from './SunMoveRow'
import { useState, useEffect } from 'react'
import { fetchSunMoves } from '../../../scripts/requests'
import CityPicker from './CityPicker'

function EditSunMoveForm({ cities }) {

  const [sunMoves, setSunMoves] = useState([])
  const [choosenCity, setChoosenCity] = useState({})


  useEffect(() => {
    const getInitSunmove = async () => {
      const initSunMoves = await fetchSunMoves(cities[0].name, localStorage.getItem("userToken"))
      setSunMoves(initSunMoves)
    }
    getInitSunmove()
  }, [])

  const handleSearch = async (cityName) => {
    const sunMoves = await fetchSunMoves(cityName, localStorage.getItem("userToken"))
    const updatedChoosenCity = cities.find(c => c.name === cityName)
    setChoosenCity(updatedChoosenCity)
    setSunMoves(sunMoves)
  }


  return (
    <div className='edit-form sun-form'>
      <CityPicker cities={cities} handleSearch={handleSearch} />
      {sunMoves && sunMoves.map(sunMove => (
        <SunMoveRow key={sunMove.id} sunMove={sunMove} choosenCity={choosenCity} />
      ))}
    </div>
  )
}

export default EditSunMoveForm