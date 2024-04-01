import {Layout, Row} from "antd";
import React, {useEffect} from "react";
import "./App.css";
import Navbar from "./components/Navbar";
import AppRouters from "./components/AppRouters";
import {setIsAuth} from "./store/reducers/AuthSlicer";
import {useAppDispatch} from "./hooks/redux";

function App() {
    const dispatch = useAppDispatch();
    useEffect(() => {
        if (localStorage.getItem("token") !== null) {
            //console.log("App.tsx");
            dispatch(setIsAuth(true));
        }
    }, []);
    return (

        <Layout>
                <Navbar/>
            <Layout.Content>
                <AppRouters/>
            </Layout.Content>
        </Layout>


    );
}

export default App;
