import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { basketActions } from '../../+store/actions';
import {
    Menu,
    IconButton,
    Badge,
    Button,
    List,
    ListItem,
    ListItemAvatar,
    ListItemSecondaryAction,
    ListItemText,
    Avatar,
    Divider,
} from '@material-ui/core';
import DeleteIcon from '@material-ui/icons/Delete';
import AddShoppingCartIcon from '@material-ui/icons/AddShoppingCart';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import useStyles from '../../style/Landing/main';

export const CustomShoppingCart = () => {
    const classes = useStyles();
    const dispatch = useDispatch();

    const notification = useSelector(state => state.basket.total);
    const items = useSelector(state => state.basket.items);

    const [notificationCount, setNotificationCount] = useState(0);
    const [products, setProducts] = useState([]);

    const [anchorEl, setAnchorEl] = useState(null);
    const isMenuOpen = Boolean(anchorEl);

    useEffect(() => {
        setNotificationCount(notification);
        setProducts(items);
    }, [notification, items])

    const handleProfileMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
        setNotificationCount(0);
    };

    const handleClearClick = () => {
        dispatch(basketActions.clearBasket());
    };

    const handlePlaceOrderClick = () => {
        // dispatch(basketActions.placeOrder());
    };

    const handleRemoveClick = (event) => {
        event.persist();

        const name = event.currentTarget.getAttribute("data-name");
        const id = event.currentTarget.getAttribute("data-id");

        dispatch(basketActions.removeFromBasket({name, id}));
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
            <List>
            {
                products &&
                products.map(pr =>
                        <ListItem key={pr.id}>
                            <ListItemAvatar>
                                <Avatar>
                                    <AddShoppingCartIcon />
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText
                                primary={pr.name}
                            />
                            <ListItemSecondaryAction>
                                <IconButton
                                    edge="end"
                                    aria-label="delete"
                                    data-id={pr.id}
                                    data-name={pr.name}
                                    onClick={handleRemoveClick}
                                >
                                    <DeleteIcon />
                                </IconButton>
                            </ListItemSecondaryAction>
                        </ListItem>
                    )
            }
            {
                notificationCount > 0 &&
                <>
                    <Divider />
                    <ListItem
                        button
                        onCLick={handleClearClick}
                    >
                        <Button
                            className={classes.addToBasket}
                            variant="contained"
                            onClick={handleClearClick}
                        >
                            Empty Cart
                        </Button>
                    </ListItem>
                    <ListItem
                        button
                        onCLick={handleClearClick}
                    >
                        <Button
                            className={classes.addToBasket}
                            variant="contained"
                            color="primary"
                            onClick={handlePlaceOrderClick}
                        >
                            Place Order
                        </Button>
                    </ListItem>
                </>
            }
            </List>
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
                    <ShoppingCartIcon />
                </Badge>
            </IconButton>
            {renderMenu}
        </React.Fragment>
    )
}