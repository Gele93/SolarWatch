import React, { useState } from 'react'
import CityRow from './CityRow'

function EditCitiesForm({ cities, setCities }) {

  return (
    <div className='edit-form'>
      {cities && cities.map(city => (
        <CityRow key={city.id} city={city} setCities={setCities} cities={cities} />
      ))}
    </div>
  )
}

export default EditCitiesForm