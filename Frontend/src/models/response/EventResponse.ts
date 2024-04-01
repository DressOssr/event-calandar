import {IError} from "../IError";

export interface EventResponse{
    id:number,
    eventName:string,
    description:string,
    eventStart:string,
    eventEnd:string,
    error:IError
}