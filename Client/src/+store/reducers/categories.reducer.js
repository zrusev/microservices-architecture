import { categoriesConstants } from '../../constants';

const initialState = {
        categories: [],
      };

export const categories = (state = initialState, action) => {
  switch (action.type) {
    case categoriesConstants.CREATE_REQUEST:
    case categoriesConstants.GET_REQUEST:
      return {
        ...state,
        categories: action.categories,
      };
    case categoriesConstants.CREATE_SUCCESS:
    case categoriesConstants.GET_SUCCESS:
      return {
        ...state,
        categories: action.categories,
      };
    case categoriesConstants.CREATE_FAILURE:
    case categoriesConstants.GET_FAILURE:
      return {};
    default:
      return state
  }
}