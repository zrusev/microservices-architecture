import * as signalR from "@microsoft/signalr";
import { notificationsServiceBaseURL } from '../../constants';
import { notificationsActions } from '../actions/index';

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${notificationsServiceBaseURL}/notifications`)
    .build();

export const signalRInvokeMiddleware = (store) => {
    return (next) => async (action) => {
        switch (action.type) {
            case "SIGNALR_CALL_Hub":
                connection.invoke('HubIsCalled');
                break;
            default:
        }
        return next(action);
    }
}

export const signalRRegisterCommands = (store, callback) => {
    connection.on('ReceiveNotification', data => {
        store.dispatch(notificationsActions.success(data))
    });

    connection.start().then(callback());
}