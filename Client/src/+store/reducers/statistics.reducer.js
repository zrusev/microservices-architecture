import { statistictsConstants } from '../../constants';

const initialState = {
      totalVisits: {},
    };

export const statistics = (state = initialState, action) => {
  switch (action.type) {
    case statistictsConstants.CREATE_REQUEST:
    case statistictsConstants.GET_REQUEST:
      return {
        ...state,
        seenProduct: action.seenProduct,
      };
    case statistictsConstants.CREATE_SUCCESS:
    case statistictsConstants.GET_SUCCESS:
      return {
        ...state,
        seenProduct: action.seenProduct,
      };
    case statistictsConstants.CREATE_FAILURE:
    case statistictsConstants.GET_FAILURE:
      return {};
    default:
      return state
  }
}