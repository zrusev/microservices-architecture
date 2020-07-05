import { customerConstants } from '../../constants';

const initialState = {
        customer: null,
    };

export const customerCreation = (state = initialState, action) => {
  switch (action.type) {
    case customerConstants.CREATE_REQUEST:
    case customerConstants.GET_REQUEST:
      return {
        ...state,
        customer: action.customer,
      };
    case customerConstants.CREATE_SUCCESS:
    case customerConstants.GET_SUCCESS:
      return {
        ...state,
        customer: action.customer,
      };
    case customerConstants.CREATE_FAILURE:
    case customerConstants.GET_FAILURE:
      return {};
    default:
      return state
  }
}