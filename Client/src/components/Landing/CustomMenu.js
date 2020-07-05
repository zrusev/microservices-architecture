import React from 'react';
import { useSelector } from 'react-redux';
import { NavLink } from 'react-router-dom';
import {
    Link,
    Menu,
    MenuItem,
} from '@material-ui/core';
import { Logout } from '../index';

export const CustomMenu = ({menuId, anchorEl, isMenuOpen, handleMenuClose}) => {
    const loggedIn = useSelector(state => state.authentication.loggedIn);

    return (
        <Menu
            anchorEl={anchorEl}
            anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
            id={menuId}
            keepMounted
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            open={isMenuOpen}
            onClose={handleMenuClose}
        >
            {
                loggedIn ?
                <div>
                    <Link color="textPrimary" component={NavLink} to="/profile">
                        <MenuItem onClick={handleMenuClose}>Profile</MenuItem>
                    </Link>
                    <Logout handleMenuClose={handleMenuClose} />
                </div> :
                <div>
                    <Link color="textPrimary" component={NavLink} to="/login">
                        <MenuItem onClick={handleMenuClose}>Login</MenuItem>
                    </Link>
                    <Link color="textPrimary" component={NavLink} to="/register">
                        <MenuItem onClick={handleMenuClose}>Register</MenuItem>
                    </Link>
                </div>
            }
      </Menu>
    )
}