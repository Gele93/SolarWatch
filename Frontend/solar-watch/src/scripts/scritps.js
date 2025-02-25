export const convertTo24h = (dateString) => {
    if (!dateString) return null
    if (dateString.split(" ").includes("AM")) {
        return dateString.split(" ")[0]
    } else {
        let nakedDate = dateString.split(" ")[0]
        let digits = nakedDate.split(":")
        let hour = parseInt(digits[0])
        hour += 12
        digits[0] = hour.toString()
        return digits.join(":")
    }
}