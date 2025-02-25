import React, { useState } from 'react'
import { fetchPutSunMove } from '../../../scripts/requests'

function SunMoveRow({ sunMove, choosenCity }) {

    console.log(sunMove)

    const [date, setDate] = useState(sunMove ? sunMove.date.slice(0, 10) : "")
    const [sunset, setSunset] = useState(sunMove.sunset)
    const [sunrise, setSunrise] = useState(sunMove.sunRise)


    const handleUpdate = async () => {
        const sunMoveData = { sunset, sunrise }
        const updatedSunMove = await fetchPutSunMove(choosenCity.id, date, sunMoveData, localStorage.getItem("userToken"))
        console.log(updatedSunMove)
    }

    return (
        <div className='sunmove row' >
            <div className='inputs'>
                <input value={date ?? ""} type='date' onChange={(e) => setDate(e.target.value)} />
                <input value={sunrise ?? ""} type='text' onChange={(e) => setSunrise(e.target.value)} />
                <input value={sunset ?? ""} type='text' onChange={(e) => setSunset(e.target.value)} />
            </div>
            <button onClick={() => handleUpdate()}>Update</button>
        </div>
    )
}

export default SunMoveRow