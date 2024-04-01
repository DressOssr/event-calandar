import React, {FC} from "react";
import {Button, DatePicker, Form, Input, Row, Space} from "antd";
import {useAppDispatch, useAppSelector} from "../hooks/redux";
import {IEvent} from "../models/IEvent";
import dayjs from "dayjs";
import EventService from "../services/EventService";
import {EventRequest} from "../models/request/EventRequest";
import { setIsModelOpen} from "../store/reducers/ModalSlicer";


interface EventFormProps {
    event: IEvent,
}

//TODO добавить TimePicker
//Форма для CRUD операций с событиями
const EventForm: FC<EventFormProps> = (props) => {
    const {isLoading} = useAppSelector(state => state.eventReducer);
    const {idEventInModal} = useAppSelector(state => state.modalReducer);
    const dispatch = useAppDispatch();
    const onFinish = (values: EventRequest) => {
        values.ts = new Date().toISOString();
        console.log("amdioawfmawkl;");
        if (idEventInModal === 0) {
            values.id = idEventInModal;
            dispatch(EventService.addEvent(values));
        } else {
            values.id = idEventInModal;
            dispatch(EventService.editEvent(values));
        }
        dispatch(setIsModelOpen(false));
    };

    return (
        <Form
            onFinish={onFinish}
        >
            <Form.Item
                label="EventName"
                name="eventName"
                rules={[{required: true, message: "Please input your tittle!"}]}
                initialValue={props.event?.eventName}
            >
                <Input/>
            </Form.Item>

            <Form.Item
                label="Description"
                name="description"
                rules={[{required: true, message: "Please input event description!"}]}
                initialValue={props.event?.description || ""}
            >
                <Input/>
            </Form.Item>

            <Form.Item
                label="Start Event"
                name="eventStart"
                rules={[{required: true, message: "Please input event started!"}]}
                initialValue={dayjs(props.event?.eventStart)}
            >
                <DatePicker/>
            </Form.Item>

            <Form.Item
                label="End Event"
                name="eventEnd"
                rules={[{required: true, message: "Please input event ending!"}]}
                initialValue={dayjs(props.event?.eventEnd)}
            >
                <DatePicker/>
            </Form.Item>
            <Row align={"middle"}>
                {idEventInModal === 0
                    ?
                    <Form.Item wrapperCol={{offset: 8, span: 16}}>
                        <Button type="primary" htmlType="submit" loading={isLoading}>
                            Add
                        </Button>
                    </Form.Item>
                    :
                    <>
                        <Space>
                            <Form.Item wrapperCol={{offset: 8, span: 16}}>
                                <Button type="primary" htmlType="submit" loading={isLoading}>
                                    Edit
                                </Button>
                            </Form.Item>
                        </Space>
                        <Space>
                            <Form.Item wrapperCol={{offset: 8, span: 16}}>
                                <Button
                                    type="primary"
                                    htmlType="button"
                                    onClick={() => {
                                        dispatch(EventService.deleteEvent(idEventInModal));
                                        dispatch(setIsModelOpen(false));
                                    }}
                                    loading={isLoading}
                                >
                                    Delete
                                </Button>
                            </Form.Item>
                        </Space>
                    </>
                }
            </Row>
        </Form>
    );
};

export default EventForm;