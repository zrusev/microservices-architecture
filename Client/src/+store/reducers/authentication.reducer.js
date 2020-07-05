import { identityConstants } from '../../constants';

let auth_token = localStorage.getItem('auth_token');

const initialState = auth_token ? {
        loggedIn: true,
        auth_token,
    } : {
      loggedIn: false,
      auth_token: '',
    };

export const authentication = (state = initialState, action) => {
  switch (action.type) {
    case identityConstants.LOGIN_REQUEST:
    case identityConstants.LOGIN_FACEBOOK_REQUEST:
    case identityConstants.REGISTER_REQUEST:
      return {
        ...state,
        loggingIn: true,
        auth_token: action.user.token,
      };
    case identityConstants.LOGIN_SUCCESS:
    case identityConstants.LOGIN_FACEBOOK_SUCCESS:
    case identityConstants.REGISTER_SUCCESS:
      return {
        ...state,
        loggingIn: false,
        loggedIn: true,
        auth_token: action.user.token,
      };
    case identityConstants.LOGIN_FAILURE:
    case identityConstants.LOGIN_FACEBOOK_FAILURE:
    case identityConstants.REGISTER_FAILURE:
      return {};
    case identityConstants.LOGOUT:
      return {};
    default:
      return state
  }
}