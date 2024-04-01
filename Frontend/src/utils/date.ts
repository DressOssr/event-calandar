import moment, {Moment} from "moment";
//TODO валидация DatePicker
export const rules = {
    required: (message: string = "Required input") => ({
        required: true,
        message
    }),
    isDateAfter: (message: string = "Can't create an event in the past") => () => ({
        validator(_: any, value: Moment) {

            if (value.isSameOrAfter(moment())) {
                console.log(value);
                //return Promise.resolve()
            }
            return Promise.reject(new Error(message));
        }
    })
}
