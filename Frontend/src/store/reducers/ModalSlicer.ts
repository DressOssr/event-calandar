import {IModal} from "../../models/IModal";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";

const initialState:IModal = {
    idEventInModal: 0,
    isModalOpen:false
}

const modalSlicer = createSlice({
    name: "modal",
    initialState,
    reducers:{
        setIdEventInModal(state,action:PayloadAction<number>){
            state.idEventInModal = action.payload
        },
        setIsModelOpen(state,action:PayloadAction<boolean>){
            state.isModalOpen = action.payload
        },
    }
})
export default modalSlicer.reducer;
export const {setIdEventInModal,setIsModelOpen} = modalSlicer.actions;
