import React, { useEffect, useState } from 'react'
import { useNavigate } from "react-router-dom";
import SunAnimationContainer from '../containers/SunAnimationContainer';
import HeaderBar from '../components/HeaderBar';
import EditCitiesForm from '../components/EditData/Cities/EditCitiesForm';
import EditSunMoveForm from '../components/EditData/SunMoves/EditSunMoveForm';
import EditTypeButtons from '../components/EditData/EditTypeButtons';
import { fetchCities } from '../scripts/requests';

function EditData({ userProps }) {

    const { user, setUser, userRole, setUserRole } = userProps

    const navigate = useNavigate()

    const [isEditCities, setIsEditCities] = useState(true)
    const [cities, setCities] = useState([])

    useEffect(() => {
        if (userRole) {
            if (userRole !== "Admin") {
                navigate("/sun")
            }
        }
    }, [userRole])

    useEffect(() => {
        const getCities = async () => {
            const cities = await fetchCities(localStorage.getItem("userToken"))
            setCities(cities)
        }
        getCities()
    }, [])

    return (
        <SunAnimationContainer>
            <HeaderBar userProps={userProps} headerText={"Edit Data"} />
            <div className='edit-data-container'>
                <EditTypeButtons setIsEditCities={setIsEditCities} />
                {isEditCities ?
                    <EditCitiesForm cities={cities} setCities={setCities} />
                    :
                    <EditSunMoveForm cities={cities}/>
                }
            </div>
        </SunAnimationContainer>
    )
}

export default EditData