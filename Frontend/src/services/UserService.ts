import {AxiosResponse} from "axios";
import {UserResponse} from "../models/response/UserResponse";
import $api from "../http";

export default class UserService{
    static async fetchUsers():Promise<AxiosResponse<UserResponse>>{
        return $api.get<UserResponse>('/users/info');
    }
}