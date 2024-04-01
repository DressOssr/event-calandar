import React from "react";
import {Button, Form, Input} from "antd";
import {useAppDispatch, useAppSelector} from "../hooks/redux";
import AuthService from "../services/AuthService";
import {LoginRequest} from "../models/request/LoginRequest";

const LoginForm: React.FC = () => {

    const dispatch = useAppDispatch();
    const {isLoading, error} = useAppSelector(state => state.authReducer);
    const onFinish = (values: LoginRequest) => {
        dispatch(AuthService.login(values));
    };
    return (
        <Form
            style={{ maxWidth: 600 }}
            onFinish={onFinish}
        >
            {error && <div style={{color: "red"}}>
                {error.errorMessage}
            </div>}

            <Form.Item
                label="Email"
                name="email"
                rules={[{required: true, message: "Please input your Email!", type: "email"}]}

            >
                <Input/>
            </Form.Item>

            <Form.Item
                label="Password"
                name="password"
                rules={[{required: true, message: "Please input your password!"}]}
            >
                <Input.Password/>
            </Form.Item>

            <Form.Item wrapperCol={{offset: 8, span: 16}}>
                <Button type="primary" htmlType="submit" loading={isLoading}>
                    Login
                </Button>
            </Form.Item>
        </Form>
    );
};

export default LoginForm;