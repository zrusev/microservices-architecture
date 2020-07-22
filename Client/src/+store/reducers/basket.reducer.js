import { basketConstants } from '../../constants';

export const initialState = {
        items: [],
        total: 0
    };

export const basket = (
    state = JSON.parse(window.localStorage.getItem('user_basket')) || initialState,
    action
  ) => {
  switch (action.type) {
    case basketConstants.ADD_REQUEST:
    case basketConstants.ADD_SUCCESS:
    case basketConstants.REMOVE_REQUEST:
    case basketConstants.REMOVE_SUCCESS:
    case basketConstants.CLEAR_REQUEST:
    case basketConstants.CLEAR_SUCCESS:
      return {
        ...state,
        items: action.items,
        total: action.items.length
      };
    case basketConstants.ADD_FAILURE:
    case basketConstants.REMOVE_FAILURE:
    case basketConstants.CLEAR_FAILURE:
    return {};
    default:
      return state
  }
}