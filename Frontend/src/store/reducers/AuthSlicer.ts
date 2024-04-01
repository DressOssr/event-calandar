import {createSlice, PayloadAction} from "@reduxjs/toolkit";

import {AuthResponse} from "../../models/response/AuthResponse";
import AuthService from "../../services/AuthService";
import {IError} from "../../models/IError";
import {IAuth} from "../../models/IAuth";


const initialState: IAuth = {
    userId: 0,
    isAuth: false,
    isLoading: false,
    error: {} as IError
};
export const authSlicer = createSlice({
    name: "auth",
    initialState,
    reducers: {
        setIsLoading: (state, action: PayloadAction<boolean>) => {
            state.isLoading = action.payload;
        },
        setIsAuth: (state, action: PayloadAction<boolean>) => {
            state.isAuth = action.payload;
        }
    },
    //fulfilled - успешное выполнения ,pending - момент выполнения, rejected-ошибка
    //обработка всех сценариев
    extraReducers: (builder) => {
        builder.addCase(AuthService.login.fulfilled.type, (state, action: PayloadAction<AuthResponse>) => {
            state.isLoading = false;
            state.isAuth = true;
            state.userId = action.payload.userId;
            state.error = {} as IError;
        }).addCase(AuthService.login.pending.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = true;
        }).addCase(AuthService.login.rejected.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = false;
            state.error = action.payload;
        }).addCase(AuthService.signup.fulfilled.type, (state) => {
            state.isLoading = false;
            state.error = {} as IError;
        }).addCase(AuthService.signup.pending.type, (state) => {
            state.isLoading = true;
        }).addCase(AuthService.signup.rejected.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = false;
            state.error = action.payload;
        }).addCase(AuthService.logout.fulfilled.type, (state) => {
            state.isAuth = false;
            state.isLoading = false;
        }).addCase(AuthService.logout.pending.type, (state) => {
            state.isLoading = true;
        }).addCase(AuthService.logout.rejected.type, (state) => {
            state.isAuth = false;
            state.isLoading = false;
        });


    }

});

export default authSlicer.reducer;
export const {setIsLoading, setIsAuth} = authSlicer.actions;
