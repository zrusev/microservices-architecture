import React from 'react';
import { useDispatch } from 'react-redux';
import { identityActions } from '../../+store/actions';
import { history } from '../../helpers';
import {
    Link,
    MenuItem,
} from '@material-ui/core';

export const Logout = ({handleMenuClose}) => {
    const dispatch = useDispatch();

    const logOutUser = () => {
        handleMenuClose();

        dispatch(identityActions.logout());

        history.push("/");
    };

    return (
        <Link color="textPrimary">
            <MenuItem onClick={() => logOutUser()}>Logout</MenuItem>
        </Link>
    );
};