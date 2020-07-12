import React, { useState, useEffect } from 'react';
import { NavLink } from 'react-router-dom';
import { useSelector } from 'react-redux';
import {
    Badge,
    Menu,
    MenuItem,
    Divider,
    Link,
} from '@material-ui/core';
import IconButton from '@material-ui/core/IconButton';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import AccountCircle from '@material-ui/icons/AccountCircle';
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';
import NotificationsIcon from '@material-ui/icons/Notifications';
import {
    ThemeSwitch,
    Logout,
} from '../index';

export const CustomMenuMobile = ({
    mobileMoreAnchorEl,
    mobileMenuId,
    isMobileMenuOpen,
    handleMobileMenuClose,
    handleProfileMenuOpen,
    handleMenuClose
 }) => {
    const loggedIn = useSelector(state => state.authentication.loggedIn);
    const notification = useSelector(state => state.notification.message);

    const [notificationCount, setNotificationCount] = useState(0);

    useEffect(() => {
        setNotificationCount(1);
    }, [notification])

    return (
        <Menu
            anchorEl={mobileMoreAnchorEl}
            anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
            id={mobileMenuId}
            keepMounted
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            open={isMobileMenuOpen}
            onClose={handleMobileMenuClose}
        >
            <MenuItem>
                <IconButton aria-label="show 4 new mails" color="inherit">
                    <Badge badgeContent={4} color="secondary">
                    <ShoppingCartIcon />
                    </Badge>
                </IconButton>
                <p>Cart</p>
            </MenuItem>
            <MenuItem>
                <IconButton aria-label="show {{notificationCount}} new notifications" color="inherit">
                    <Badge badgeContent={notificationCount} color="secondary">
                        <NotificationsIcon />
                    </Badge>
                </IconButton>
                <p>Notifications</p>
            </MenuItem>
            {
                loggedIn ?
                <div>
                    <MenuItem onClick={handleProfileMenuOpen}>
                        <IconButton
                            aria-label="account of current user"
                            aria-controls="primary-search-account-menu"
                            aria-haspopup="true"
                            color="inherit"
                            >
                            <AccountCircle />
                        </IconButton>
                        <p>Profile</p>
                    </MenuItem>
                    <Logout handleMenuClose={handleMenuClose} />
                </div> :
                <div>
                    <MenuItem onClick={handleMenuClose}>
                        <IconButton>
                            <AccountBoxIcon />
                        </IconButton>
                        <Link color="textPrimary" component={NavLink} to="/login">
                            <p>Login</p>
                        </Link>
                    </MenuItem>
                    <MenuItem onClick={handleMenuClose}>
                        <IconButton>
                            <AssignmentIndIcon />
                        </IconButton>
                        <Link color="textPrimary" component={NavLink} to="/register">
                            <p>Register</p>
                        </Link>
                    </MenuItem>
                </div>
            }
            <Divider />
            <MenuItem>
                <IconButton aria-label="theme switch" color="inherit">
                    <ThemeSwitch />
                </IconButton>
                <p>Dark mode</p>
            </MenuItem>
        </Menu>
    )
};