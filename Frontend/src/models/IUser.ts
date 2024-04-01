import {IError} from "./IError";

export interface IUser{
    email: string,
    firstName: string,
    lastName: string,
    isLoading:boolean,
    error:IError
}