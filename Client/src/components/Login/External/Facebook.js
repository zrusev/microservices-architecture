import React from 'react';
import FacebookLogin from 'react-facebook-login';
import { history } from '../../../helpers';
import { useDispatch } from 'react-redux';
import { identityActions, alertActions } from '../../../+store/actions';
import useStyles from '../../../style/Login/External/facebook';

export const Facebook = () => {
    const classes = useStyles();
    const dispatch = useDispatch();

    const handleResFailure = () => {
        history.push("/");

        dispatch(alertActions.error([ 
            { 
                code: 'InvalidFacebookAuthentication',
                description: 'Invalid authentication at Facebook'
            } 
        ]));
    }

    const handleResSuccess = response => {
        const { accessToken } = response;

        if(accessToken)
            dispatch(identityActions.loginWithFacebook(accessToken));

        history.push("/");
    }

    return (
        <div className={classes.btn}>
            <FacebookLogin
                appId={process.env.REACT_APP_FB_APP_ID}
                callback={handleResSuccess}
                onFailure={handleResFailure}
                size='small'
                textButton='Continue with Facebook'
                scope='email'
                cssClass='kep-login-facebook'
            />
        </div>
    )
};