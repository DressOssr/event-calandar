import React from "react";
import {Button, Form, Input} from "antd";
import AuthService from "../services/AuthService";
import {useAppDispatch, useAppSelector} from "../hooks/redux";
import {SignupRequest} from "../models/request/SignupRequest";

const SignupForm = () => {
    const onFinish = (values: SignupRequest) => {
        values.ts = new Date().toISOString();
        dispatch(AuthService.signup(values));
    };
    const dispatch = useAppDispatch();
    const {isLoading, error} = useAppSelector(state => state.authReducer);
    return (
        <Form
            onFinish={onFinish}
        >
            {error && <div style={{color: "red"}}>
                {error.errorMessage}
            </div>}
            <Form.Item
                name="email"
                label="E-mail"
                rules={[
                    {
                        type: "email",
                        message: "The input is not valid E-mail!",
                    },
                    {
                        required: true,
                        message: "Please input your E-mail!",
                    },
                ]}
            >
                <Input/>
            </Form.Item>

            <Form.Item
                name="password"
                label="Password"
                rules={[
                    {
                        required: true,
                        message: "Please input your password!",
                    },
                ]}
                hasFeedback
            >
                <Input.Password/>
            </Form.Item>

            <Form.Item
                name="confirmPassword"
                label="Confirm Password"
                dependencies={["password"]}
                hasFeedback
                rules={[
                    {
                        required: true,
                        message: "Please confirm your password!",
                    },
                    ({getFieldValue}) => ({
                        validator(_, value) {
                            if (!value || getFieldValue("password") === value) {
                                return Promise.resolve();
                            }
                            return Promise.reject(new Error("The two passwords that you entered do not match!"));
                        },
                    }),
                ]}
            >
                <Input.Password/>
            </Form.Item>

            <Form.Item
                name="firstName"
                label="FirstName"
                rules={[{required: true, message: "Please input your FirstName!", whitespace: true}]}
            >
                <Input/>
            </Form.Item>
            <Form.Item
                name="lastName"
                label="LastName"
                rules={[{required: true, message: "Please input your LastName!", whitespace: true}]}
            >
                <Input/>
            </Form.Item>

            <Form.Item wrapperCol={{offset: 8, span: 16}}>
                <Button type="primary" htmlType="submit" loading={isLoading}>
                    Signup
                </Button>
            </Form.Item>
        </Form>
    );
};

export default SignupForm;