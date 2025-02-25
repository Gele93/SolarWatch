import React from 'react'
function SunMoveForm({ handleCityChange, setDate, citySuggestions }) {
    return (
        <form>
            <div className='row'>
                <label htmlFor='city'>City</label>
                <input list="city-names" autoComplete="off" id='city' type='text' onChange={(e) => handleCityChange(e.target.value)} />
                <datalist id='city-names'>
                    {citySuggestions && citySuggestions.map(c => (
                        <option value={c.name} />
                    ))}
                </datalist>
            </div>
            <div className='row'>
                <label htmlFor='date'>Date</label>
                <input autoComplete="off" id='date' type='date' onChange={(e) => setDate(e.target.value)} />
            </div>
        </form>
    )
}

export default SunMoveForm