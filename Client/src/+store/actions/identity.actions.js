import { identityConstants } from '../../constants';
import { IdentityService } from '../../services';
import { alertActions } from './alert.actions';

const service = new IdentityService();

export const identityActions = {
    loginWithFacebook: accessToken => {
        const request = user => ({ type: identityConstants.LOGIN_FACEBOOK_REQUEST, user }); 
        const success = user => ({ type: identityConstants.LOGIN_FACEBOOK_SUCCESS, user });
        const failure = error => ({ type: identityConstants.LOGIN_FACEBOOK_FAILURE, error });

        return dispatch => {
            dispatch(request(accessToken));

            service.login({ accessToken, facebookLoginUrl: true })
                .then(
                    user => { 
                        dispatch(success(user));
    
                        if (user.token) {
                            window.localStorage.setItem('auth_token', user.token);
                        }
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
    login: (email, password) => {
        const request = user => ({ type: identityConstants.LOGIN_REQUEST, user }); 
        const success = user => ({ type: identityConstants.LOGIN_SUCCESS, user });
        const failure = error => ({ type: identityConstants.LOGIN_FAILURE, error });

        return dispatch => {
            dispatch(request({ email }));
    
            service.login({ email, password })
                .then(
                    user => { 
                        dispatch(success(user));

                        if (user.token) {
                            window.localStorage.setItem('auth_token', user.token);
                        }
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
    logout: () => {
        service.logout();

        return { type: identityConstants.LOGOUT };
    },
    register: (email, password) => {
        const request = user => ({ type: identityConstants.REGISTER_REQUEST, user }); 
        const success = user => ({ type: identityConstants.REGISTER_SUCCESS, user });
        const failure = error => ({ type: identityConstants.REGISTER_FAILURE, error });
        
        return dispatch => {
            dispatch(request({ email }));
    
            service.register({ email, password })
                .then(
                    user => { 
                        dispatch(success(user));

                        if (user.token) {
                            window.localStorage.setItem('auth_token', user.token);
                        }
                    },
                    error => {
                        dispatch(failure(error));
                        dispatch(alertActions.error(error));
                    }
                );
        }
    },
};