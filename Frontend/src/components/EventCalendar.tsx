import FullCalendar from "@fullcalendar/react";
import React, {FC} from "react";
import dayGridPlugin from "@fullcalendar/daygrid";
import {IEvent} from "../models/IEvent";
import {setIdEventInModal, setIsModelOpen} from "../store/reducers/ModalSlicer";
import {useAppDispatch} from "../hooks/redux";

interface EventCalendarProps {
    events: IEvent[];
}

//мой календарь
const EventCalendar: FC<EventCalendarProps> = (props) => {
    const dispatch = useAppDispatch();
    const arr = props.events.map((event) => ({
        id:event.id?.toString() || '',
        title: event.eventName,
        start: event.eventStart,
        end: event.eventEnd,
        description: event.description
    }));
    return (
        <FullCalendar
            plugins={[dayGridPlugin]}
            events={arr}
            eventClick={(e) => {
                dispatch(setIsModelOpen(true));
                dispatch(setIdEventInModal(Number(e.event.id)));
            }
            }/>
    );
};

export default EventCalendar;