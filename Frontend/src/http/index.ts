import axios from "axios";
import {AuthResponse} from "../models/response/AuthResponse";
import jwt_decode, {JwtPayload} from "jwt-decode";


export const API_URL = 'https://localhost:7023/api'

const $api = axios.create({
    withCredentials:true,
    baseURL:API_URL
})
//добавлят токен к запросу
$api.interceptors.request.use((config)=>{
    config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`;
    return config;
});
//перехватывает 401 error code и посылает refresh запрос и делает еще раз прошлый запрос
$api.interceptors.response.use((config) => {
    return config;
},async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && error.config && !error.config._isRetry) {
        originalRequest._isRetry = true;
        try {
            const decoded = jwt_decode<JwtPayload>(localStorage.getItem("token") || '') || null;
            // @ts-ignore
            let userId = decoded.nameid;

            const response = await axios.post<AuthResponse>(`${API_URL}/users/refresh`, {userId:userId},{withCredentials:true})
            localStorage.setItem('token', response.data.accessToken);
            return $api.request(originalRequest);
        } catch (e) {
            localStorage.removeItem('token');
        }
    }
    localStorage.removeItem('token');
    throw error;
})
export default $api;
