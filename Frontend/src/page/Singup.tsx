import React from "react";
import {Card, Layout, Row} from "antd";
import SignupForm from "../components/SignupForm";

const Singup = () => {
    return (
        <Layout>
            <Row justify="center" align="middle" className="h100">
                <Card>
                    <SignupForm/>
                </Card>
            </Row>
        </Layout>
    );
};

export default Singup;