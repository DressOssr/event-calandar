import {IError} from "../IError";

export interface AuthResponse {
    accessToken: string,
    refreshToken: string,
    userId: number,
    error: IError
}