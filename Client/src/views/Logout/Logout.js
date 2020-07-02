import React from 'react';
import { useDispatch } from 'react-redux';
import { identityActions } from '../../+store/actions';
import { history } from '../../helpers';

export const LogOutPage = () => {
    const dispatch = useDispatch();

    const logOutUser = () => {
        dispatch(identityActions.logout());
    
        history.push("/");
    };

    return (
        <button className="btn btn-primary" onClick={() => logOutUser()}>Log Out</button>
    );
};