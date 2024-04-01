import React from "react";
import {Row, Spin} from "antd";
import "./Loader.module.css";

const App: React.FC = () => (
    <Row justify={"center"}>
            <Spin size={"large"}
                style={{
                    verticalAlign: 'middle',
                }}
            />
    </Row>
);

export default App;
