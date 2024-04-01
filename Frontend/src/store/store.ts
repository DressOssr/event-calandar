import {configureStore} from "@reduxjs/toolkit";
import AuthReducer from "./reducers/AuthSlicer";
import ModalReducer from "./reducers/ModalSlicer";
import EventReducer from "./reducers/EventSlicer";

//менеджер состоянии чтобы брать и изменять состояния с любого компонента
export const store = configureStore({
    reducer: {
        authReducer: AuthReducer,
        modalReducer:ModalReducer,
        eventReducer:EventReducer
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware(),
});

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch
export default store;