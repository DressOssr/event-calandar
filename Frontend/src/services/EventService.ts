import $api from "../http";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {EventResponse} from "../models/response/EventResponse";
import {AxiosError} from "axios";
import {EventRequest} from "../models/request/EventRequest";

export default class EventService {
    static getEvents = createAsyncThunk(
        "getEvents",
        async (_, thunkAPI) => {
            try {
                const response = await $api.get<EventResponse[]>("/event");
                return response.data;
            } catch (e) {
                const error = e as AxiosError<EventResponse[]>;
                return error?.response?.data;
            }

        }
    );
    static addEvent = createAsyncThunk(
        "addEvent",
        async (request:EventRequest, thunkAPI) => {
            try {
                const response = await $api.post<EventResponse>("/event",request);
                return response.data;
            } catch (e) {
                const error = e as AxiosError<EventResponse[]>;
                return error?.response?.data;
            }

        }
    );
    static editEvent = createAsyncThunk(
        "editEvent",
        async (request:EventRequest, thunkAPI) => {
            try {
                const response = await $api.put<EventResponse>("/event",request);
                return response.data;
            } catch (e) {
                const error = e as AxiosError<EventResponse[]>;
                return error?.response?.data;
            }

        }
    );
    static deleteEvent = createAsyncThunk(
        "deleteEvent",
        async (id:number, thunkAPI) => {
            try {
                const response = await $api.delete<EventResponse>(`/event/${id}`,);
                return response.data;
            } catch (e) {
                const error = e as AxiosError<EventResponse[]>;
                return error?.response?.data;
            }

        }
    );
}