import { manufacturersConstants } from '../../constants';

const initialState = {
        manufacturers: [],
      };

export const manufacturers = (state = initialState, action) => {
  switch (action.type) {
    case manufacturersConstants.CREATE_REQUEST:
    case manufacturersConstants.GET_REQUEST:
      return {
        ...state,
        manufacturers: action.manufacturers,
      };
    case manufacturersConstants.CREATE_SUCCESS:
    case manufacturersConstants.GET_SUCCESS:
      return {
        ...state,
        manufacturers: action.manufacturers,
      };
    case manufacturersConstants.CREATE_FAILURE:
    case manufacturersConstants.GET_FAILURE:
      return {};
    default:
      return state
  }
}