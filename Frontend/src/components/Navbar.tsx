import React from "react";
import {Header} from "antd/es/layout/layout";
import {Col, Menu, Row} from "antd";
import AuthService from "../services/AuthService";
import {useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../hooks/redux";
import {setIdEventInModal, setIsModelOpen} from "../store/reducers/ModalSlicer";
import {LoginOutlined, FormOutlined, LogoutOutlined, CalendarOutlined} from "@ant-design/icons";


const Navbar = () => {
    const navigate = useNavigate();
    const {isAuth} = useAppSelector(state => state.authReducer);
    const dispatch = useAppDispatch();
    return (
        <Header>
            <Row justify="end">
                <Col span={4}>
                    {isAuth
                        ?
                        <Menu  theme="dark" mode="horizontal" selectable={false}
                              items={[{
                                  label: "Add Event",
                                  key: "addEvent",
                                  icon: <CalendarOutlined/>,
                                  onClick: () => {
                                      dispatch(setIdEventInModal(0));
                                      dispatch(setIsModelOpen(true));
                                  }
                              }, {
                                  label: "Logout",
                                  key: "logout",
                                  icon: <LogoutOutlined/>,
                                  onClick: () => dispatch(AuthService.logout())
                              }]}
                        />
                        :
                        <Menu theme="dark" mode="horizontal" selectable={false}
                              items={[{
                                  label: "SignIn",
                                  key: "signIn",
                                  icon: <LoginOutlined/>,
                                  onClick: () => navigate("login")
                              }, {
                                  label: "SignUp",
                                  key: "signUp",
                                  icon: <FormOutlined/>,
                                  onClick: () => navigate("signup")
                              }]}
                        />
                    }
                </Col>
            </Row>
        </Header>
    );
};

export default Navbar;