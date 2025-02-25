//Email, Password
export const fetchLogin = async (loginData) => {
    const resp = await fetch("/api/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(loginData)
    })
    if (!resp.ok) {
        return null
    }
    const user = await resp.json()
    return user
}

//Username, Email, Password
export const fetchRegister = async (registerData) => {
    const resp = await fetch("/api/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(registerData)
    })
    if (!resp.ok) {
        return null
    }
    const user = await resp.json()
    return user
}

export const fetchSunData = async (city, dateString, token) => {
    const response = await fetch(`/api/sun?city=${city}&date=${dateString}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`,  // Include the JWT token in Authorization header
            'Content-Type': 'application/json',
        },
    });
    if (!response.ok) {
        return null
    }
    const sunData = await response.json()
    return sunData
}

export const fetchCityNames = async (filterName, token) => {
    const response = await fetch(`/api/cityname/${filterName}`, {
        method: "GET",
        headers: {
            'Authorization': `Bearer ${token}`,  // Include the JWT token in Authorization header
            'Content-Type': 'application/json'
        }
    })
    if (!response.ok) {
        return null
    }
    const cities = await response.json()
    return cities
}

export const fetchCities = async (token) => {
    const response = await fetch(`/api/city`, {
        method: "GET",
        headers: {
            'Authorization': `Bearer ${token}`,  // Include the JWT token in Authorization header
            'Content-Type': 'application/json'
        }
    })
    if (!response.ok) {
        return null
    }
    const cities = await response.json()
    return cities
}

export const fetchPutCity = async (cityId, cityData, token) => {
    console.log(cityData)
    const response = await fetch(`/api/city/${cityId}`, {
        method: "PUT",
        headers: {
            'Authorization': `Bearer ${token}`,  // Include the JWT token in Authorization header
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(cityData)
    })
    if (!response.ok) {
        return null
    }
    const city = await response.json()
    return city
}


export const fetchSunMoves = async (city, token) => {
    const response = await fetch(`/api/sunmovement/${city}`, {
        method: "GET",
        headers: {
            'Authorization': `Bearer ${token}`,  // Include the JWT token in Authorization header
            'Content-Type': 'application/json'
        }
    })
    if (!response.ok) {
        return null
    }
    const sunmovements = await response.json()
    return sunmovements
}

export const fetchPutSunMove = async (cityId, date, sunMoveData, token) => {
    const response = await fetch(`/api/sunmovement/${cityId}/${date}`, {
        method: "PUT",
        headers: {
            'Authorization': `Bearer ${token}`,  // Include the JWT token in Authorization header
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(sunMoveData)
    })
    if (!response.ok) {
        return null
    }
    const sunmove = await response.json()
    return sunmove
}