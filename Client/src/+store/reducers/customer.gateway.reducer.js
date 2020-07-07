import { customerGatewayConstants } from '../../constants';

const initialState = {
        products: [],
    };

export const topProducts = (state = initialState, action) => {
  switch (action.type) {
    case customerGatewayConstants.TOP_PRODUCTS_REQUEST:
      return {
        ...state,
        products: action.products,
      };
    case customerGatewayConstants.TOP_PRODUCTS_SUCCESS:
      return {
        ...state,
        products: action.products,
    };
    case customerGatewayConstants.TOP_PRODUCTS_FAILURE:
      return {};
    default:
      return state
  }
}