import { orderConstants } from '../../constants';

const initialState = {
        order: {
          status: 0,
          orderDate: '',
          products: []
        }
      };

export const order = (state = initialState, action) => {
  switch (action.type) {
    case orderConstants.CREATE_REQUEST:
      return {
        ...state,
        order: action.order,
      };
    case orderConstants.CREATE_SUCCESS:
      return {
        ...state,
        order: action.order,
      };
    case orderConstants.CREATE_FAILURE:
      return {};
    default:
      return state
  }
}