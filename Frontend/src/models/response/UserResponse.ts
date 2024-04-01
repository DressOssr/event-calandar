import {IError} from "../IError";

export interface UserResponse {
    email: string,
    firstName: string,
    lastName: string,
    error: IError
}