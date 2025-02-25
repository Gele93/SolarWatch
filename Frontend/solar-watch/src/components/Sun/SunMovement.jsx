import React from 'react'
import { convertTo24h } from '../../scripts/scritps'

function SunMovement({ sunmovement }) {

    return (
        <div className='sunmovement'>
            <div className='rise'>{convertTo24h(sunmovement.sunRise)}</div>
            <div className='set'>{convertTo24h(sunmovement.sunSet)}</div>
        </div>
    )
}

export default SunMovement