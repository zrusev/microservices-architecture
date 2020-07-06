import { productsConstants } from '../../constants';

const initialState = {
        products: [],
        page: 1,
        totalProducts: 0
    };

export const products = (state = initialState, action) => {
  switch (action.type) {
    case productsConstants.CREATE_REQUEST:
    case productsConstants.GET_REQUEST:
      return {
        ...state,
        products: action.products,
      };
    case productsConstants.CREATE_SUCCESS:
    case productsConstants.GET_SUCCESS:
      return {
        ...state,
        page: action.products.page,
        totalProducts: action.products.totalProducts,
        products: action.products,
      };
    case productsConstants.CREATE_FAILURE:
    case productsConstants.GET_FAILURE:
      return {};
    default:
      return state
  }
}