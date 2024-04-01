import React from "react";
import { Route, Routes} from "react-router-dom";
import Login from "../page/Login";
import Singup from "../page/Singup";
import Event from "../page/Event";
import {useAppSelector} from "../hooks/redux";


const AppRouters = () => {
    const {isAuth} = useAppSelector(state => state.authReducer);
    return (
        <Routes>
            {isAuth ?
                <>
                    <Route path="*" element={<Event/>}/>
                    <Route path="/event" element={<Event/>}/>

                </>:
                <>
                    <Route path="*" element={<Login/>}/>
                    <Route path="/login" element={<Login/>}/>
                    <Route path="/signup" element={<Singup/>}/>
                </>
            }
        </Routes>
    );
};


export default AppRouters;