import React from 'react'
import ActionButton from '../ActionButton'

function EditTypeButtons({setIsEditCities}) {
    return (
        <div className='yellow edit-type-buttons'>
            <ActionButton action={()=> setIsEditCities(true)} buttonText="Edit cities"/>
            <ActionButton action={()=> setIsEditCities(false)} buttonText="Edit sun data"/>
        </div>
    )
}

export default EditTypeButtons