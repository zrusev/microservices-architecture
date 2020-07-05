import React from 'react';
import {
    Badge,
    Menu,
    MenuItem,
    Divider,
} from '@material-ui/core';
import IconButton from '@material-ui/core/IconButton';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import AccountCircle from '@material-ui/icons/AccountCircle';
import NotificationsIcon from '@material-ui/icons/Notifications';
import { ThemeSwitch } from '../index';

export const CustomMenuMobile = ({mobileMoreAnchorEl, mobileMenuId, isMobileMenuOpen, handleMobileMenuClose, handleProfileMenuOpen }) => (
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
        <IconButton aria-label="show 1 new notifications" color="inherit">
            <Badge badgeContent={1} color="secondary">
                <NotificationsIcon />
            </Badge>
        </IconButton>
        <p>Notifications</p>
        </MenuItem>
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
        <Divider />
        <MenuItem>
        <IconButton aria-label="theme switch" color="inherit">
            <ThemeSwitch />
        </IconButton>
        <p>Dark mode</p>
        </MenuItem>
    </Menu>
);