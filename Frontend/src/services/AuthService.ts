import $api from "../http";
import {AxiosError} from "axios";
import {AuthResponse} from "../models/response/AuthResponse";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {LoginRequest} from "../models/request/LoginRequest";
import {IError} from "../models/IError";
import {SignupRequest} from "../models/request/SignupRequest";


export default class AuthService {
  //  private static  dispatch = useAppDispatch()
    static login = createAsyncThunk(
        "users/login",
        async (request: LoginRequest, thunkAPI) => {
            try {
                const response = await $api.post<AuthResponse>("/users/login", request);
                localStorage.setItem("token", response.data.accessToken);
                return response.data;
            } catch (e) {
                const error = e as AxiosError<IError>;
                return thunkAPI.rejectWithValue(error?.response?.data);
            }
        }
    );
    static signup = createAsyncThunk(
        "users/signup",
        async (request: SignupRequest, thunkAPI) => {
            try {
                await $api.post<string>("users/signup", request);
            } catch (e) {
                const error = e as AxiosError<IError>;
                return thunkAPI.rejectWithValue(error?.response?.data);
            }
        }
    );

    static logout = createAsyncThunk(
        "users/logout",
        async (_, thunkAPI) => {
            await $api.post<void>("users/logout");
            localStorage.removeItem("token");
        }
    );
}
