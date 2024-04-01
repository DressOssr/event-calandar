import {createSlice} from "@reduxjs/toolkit";

import {IUser} from "../../models/IUser";
import {IError} from "../../models/IError";


// TODO
const initialState: IUser = {
    email: "",
    firstName: "",
    lastName: "",
    isLoading: false,
    error:{} as IError
};

export const userSlicer = createSlice({
    name: "user",
    initialState,
    reducers: {}
});

export default userSlicer.reducer;