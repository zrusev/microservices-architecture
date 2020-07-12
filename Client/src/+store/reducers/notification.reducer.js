import { notificationsConstants } from '../../constants';

export const notification = (state = {}, action) => {
  switch (action.type) {
    case notificationsConstants.SUCCESS:
      return {
        type: 'alert-success',
        message: action.message
      };
    case notificationsConstants.ERROR:
      return {
        type: 'alert-danger',
        message: action.message
      };
    case notificationsConstants.CLEAR:
      return {};
    default:
      return state
  }
}