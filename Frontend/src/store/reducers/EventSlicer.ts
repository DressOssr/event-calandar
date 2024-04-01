import {EventResponse} from "../../models/response/EventResponse";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {IEvent} from "../../models/IEvent";
import {IError} from "../../models/IError";
import EventService from "../../services/EventService";

interface EventState {
    events: IEvent[],
    isLoading: boolean,
    error: IError
}

const initialState: EventState = {
    isLoading: false,
    events: [],
    error: {} as IError
};

const eventSlicer = createSlice({
    name: "event",
    initialState,
    reducers: {
        addNewEvent(state, action: PayloadAction<EventResponse>) {
            const event: IEvent = {
                id:action.payload.id,
                eventName: action.payload.eventName,
                description: action.payload.description,
                eventStart: action.payload.eventStart,
                eventEnd: action.payload.eventEnd,
            };
            state.error = action.payload.error;
            state.events.push(event);
        },
        errorChecked(state){
            state.error = {} as IError;
        }
    }, extraReducers: (builder) => {
        builder.addCase(EventService.getEvents.fulfilled.type, (state, action: PayloadAction<EventResponse[]>) => {
            state.isLoading = false;
            state.events = action.payload.map((event)=>({
                id:event.id,
                eventName:event.eventName,
                description:event.description,
                eventStart:event.eventStart,
                eventEnd:event.eventEnd
            }))
        }).addCase(EventService.getEvents.pending.type, (state) => {
            state.isLoading = true;
        }).addCase(EventService.getEvents.rejected.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = false;
            state.error = action.payload;
        }).addCase(EventService.addEvent.fulfilled.type, (state, action: PayloadAction<EventResponse>) => {
            state.isLoading = false;
            state.events.push(action.payload)
        }).addCase(EventService.addEvent.pending.type, (state) => {
            state.isLoading = true;
        }).addCase(EventService.addEvent.rejected.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = false;
            state.error = action.payload;
        }).addCase(EventService.editEvent.fulfilled.type, (state, action: PayloadAction<EventResponse>) => {
            state.isLoading = false;
            state.events[state.events.findIndex(e=>e.id === action.payload.id)] = action.payload
        }).addCase(EventService.editEvent.pending.type, (state) => {
            state.isLoading = true;
        }).addCase(EventService.editEvent.rejected.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = false;
            state.error = action.payload;
        }).addCase(EventService.deleteEvent.fulfilled.type, (state, action: PayloadAction<number>) => {
            state.isLoading = false;
            state.events = state.events.filter(e=>e.id !== action.payload)
        }).addCase(EventService.deleteEvent.pending.type, (state) => {
            state.isLoading = true;
        }).addCase(EventService.deleteEvent.rejected.type, (state, action: PayloadAction<IError>) => {
            state.isLoading = false;
            state.error = action.payload;
        })
    }
});

export default eventSlicer.reducer;
export const {addNewEvent,errorChecked} = eventSlicer.actions;
