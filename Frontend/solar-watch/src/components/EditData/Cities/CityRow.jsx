import React, { useState } from 'react'
import { fetchPutCity } from '../../../scripts/requests'

function CityRow({ city, cities, setCities }) {

    const [id, setId] = useState(city.id)
    const [name, setName] = useState(city.name)
    const [longitude, setLongitude] = useState(city.longitude)
    const [latitude, setLatitude] = useState(city.latitude)
    const [country, setCountry] = useState(city.country)
    const [state, setState] = useState(city.state)

    const handleUpdate = async () => {
        const cityData = {
            Name: name,
            Longitude: longitude,
            Latitude: latitude,
            Country: country,
            State: state
        }
        const updatedCity = await fetchPutCity(id, cityData, localStorage.getItem("userToken"))
        let updatedCities = [...cities]
        updatedCities.map(c => c.id === id ? updatedCity : c)
        console.log(updatedCities)
        setCities(updatedCities)
    }

    return (
        <div className='city row' >
            <div className='inputs'>
                <input value={name ?? ""} type='text' onChange={(e) => setName(e.target.value)} />
                <input value={longitude ?? ""} type='text' onChange={(e) => setLongitude(e.target.value)} />
                <input value={latitude ?? ""} type='text' onChange={(e) => setLatitude(e.target.value)} />
                <input value={country ?? ""} type='text' onChange={(e) => setCountry(e.target.value)} />
                <input value={state ?? ""} type='text' onChange={(e) => setState(e.target.value)} />
            </div>
            <button onClick={() => handleUpdate()}>Update</button>
        </div>
    )
}

export default CityRow