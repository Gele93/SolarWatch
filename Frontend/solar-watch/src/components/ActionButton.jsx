import React from 'react'

function ActionButton({ buttonText, action }) {
    return (
        <div className='button-container'>
            <button onClick={() => action()}>{buttonText}</button>
        </div>
    )
}

export default ActionButton