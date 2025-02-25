import React from 'react'
import { useState } from 'react'
import ActionButton from '../components/ActionButton'

function SunAnimationContainer({children}) {
      const [isAnimation, setIsAnimation] = useState(true)

    return (
        <div className={`sun-page ${isAnimation && "animation"}`}>
            {children}
            <div className='animation-toggle yellow'>
                <ActionButton buttonText={isAnimation ? "Stop animation" : "Start animation"} action={() => setIsAnimation(!isAnimation)} />
            </div>
        </div>
    )
}

export default SunAnimationContainer