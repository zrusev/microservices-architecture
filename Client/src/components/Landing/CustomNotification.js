import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import {
    Menu,
    Typography,
    IconButton,
    Badge,
} from '@material-ui/core';
import NotificationsIcon from '@material-ui/icons/Notifications';
import useStyles from '../../style/Landing/main';

export const CustomNotification = () => {
    const classes = useStyles();

    const notification = useSelector(state => state.notification.message);

    const [notificationCount, setNotificationCount] = useState(0);
    const [anchorEl, setAnchorEl] = useState(null);
    const isMenuOpen = Boolean(anchorEl);

    useEffect(() => {
      if(notification) {
        setNotificationCount(1);
      }
    }, [notification])

    const handleProfileMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
        setNotificationCount(0);
    };

    const menuId = 'primary-notification-menu';

    const renderMenu = (
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
                notification &&
                <Typography className={classes.notify}>
                    {notification.message}
                </Typography>
            }
        </Menu>
    )

    return (
        <React.Fragment>
            <IconButton
                aria-label="show {{notificationCount}} new notifications"
                color="inherit"
                aria-controls={menuId}
                aria-haspopup="true"
                onClick={handleProfileMenuOpen}
            >
                <Badge badgeContent={notificationCount} color="secondary">
                    <NotificationsIcon />
                </Badge>
            </IconButton>
            {renderMenu}
        </React.Fragment>
    )
}