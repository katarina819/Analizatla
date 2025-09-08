import axios from "axios";

// Dinamički odabir baze URL-a
const isLocal = window.location.hostname === "localhost";

const BASE_URL = isLocal
  ? `http://${window.location.hostname}:5150/api/v1` // HTTP port za lokalni razvoj
  : 'https://analizatla.onrender.com/api/v1';



export const HttpService = axios.create({
    baseURL: BASE_URL,
    headers: {
        'Content-Type': 'application/json'
    }
});
