import React from 'react'

function CityPicker({cities, handleSearch}) {
    return (
        <div className='filter-row'>
            <label htmlFor='cities'>City</label>
            <select autoComplete="off" name="cities" id='cities' type='text' onChange={(e) => handleSearch(e.target.value)}>
                {cities && cities.map(city => (
                    <option key={city.id} value={city.name}>{city.name}</option>
                ))}
            </select>
        </div>
    )
}

export default CityPicker