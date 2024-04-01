import {Alert, Modal} from "antd";
import React, {useEffect} from "react";
import EventCalendar from "../components/EventCalendar";
import {useAppDispatch, useAppSelector} from "../hooks/redux";
import {setIsModelOpen} from "../store/reducers/ModalSlicer";
import EventForm from "../components/EventForm";
import EventService from "../services/EventService";
import Loader from "../components/UI/Loader/Loader";
import {errorChecked} from "../store/reducers/EventSlicer";

//страничка с календарьом и модальныйм окном с форомой для CRUD EVENT
const Event = () => {
    //const [alertIsOpen, setAlertIsOpen] = useState(true);
    const dispatch = useAppDispatch();
    const {isModalOpen, idEventInModal} = useAppSelector(state => state.modalReducer);
    const {events, isLoading, error} = useAppSelector(state => state.eventReducer);
    useEffect(() => {
        dispatch(EventService.getEvents());
    }, []);
    const eventAlertError = ():boolean => {
        return Object.keys(error).length !== 0
    };
    if (isLoading) {
        return <Loader/>;
    }
    return (
        <>
           {/* TODO отображение ошибки пользователю CRUD операции*/}
            <Modal
                open={eventAlertError()}
                onCancel={()=>dispatch(errorChecked())}
                destroyOnClose={true}
                footer={null}
                closable={false}
            >
                <Alert
                    message="Error"
                    description={error.errorMessage}
                    type="error"
                />
            </Modal>

            <EventCalendar events={events}/>
            <Modal
                destroyOnClose={true}
                title="Event"
                open={isModalOpen}
                footer={null}
                onCancel={() => dispatch(setIsModelOpen(false))}
                confirmLoading={true}
            >
                <EventForm event={events[events.findIndex(e => e.id === idEventInModal)]}/>
            </Modal>
        </>
    );
};

export default Event;