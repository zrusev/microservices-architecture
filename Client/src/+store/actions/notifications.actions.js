import { notificationsConstants } from '../../constants';

export const notificationsActions = {
    success: message => ({ type: notificationsConstants.SUCCESS, message }),
    error: message => ({ type: notificationsConstants.ERROR, message }),
    clear: () => ({ type: notificationsConstants.CLEAR }),
}