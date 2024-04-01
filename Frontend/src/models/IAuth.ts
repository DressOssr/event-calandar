import {IError} from "./IError";

export interface IAuth{
    userId:number,
    isAuth:boolean,
    isLoading:boolean,
    error:IError
}