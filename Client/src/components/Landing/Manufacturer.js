import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { manufacturersActions } from '../../+store/actions';
import { NavLink } from 'react-router-dom';
import {
    Link,
    Paper,
    Typography,
    Divider,
} from '@material-ui/core';
import LocationCityIcon from '@material-ui/icons/LocationCity';
import useStyles from '../../style/Landing/manufacturer';

const Item = ({item}) => {
    const classes = useStyles();
    const params = {
        page: 1,
        manufacturer: item.name.replace(/\s/g, '-'),
    }

    return (
        <Link
            className={classes.item}
            component={NavLink}
            to={{
                pathname: '/products',
                search: `?page=${params.page}&manufacturer=${params.manufacturer.toLowerCase()}`,
            }}
        >
            <Paper elevation={3} className={classes.paper}>
                <LocationCityIcon className={classes.icon} />
                <Divider />
                <Typography
                    className={classes.title}
                    variant="h6"
                    noWrap>
                    {item.name}
                </Typography>
            </Paper>
        </Link>
    )
}

export const Manufacturer = () => {
    const classes = useStyles();

    const dispatch = useDispatch();
    const manufacturers = useSelector(state => state.manufacturers.manufacturers);

    useEffect(() => {
        dispatch(manufacturersActions.topManufacturers());
    }, [dispatch]);

    if(manufacturers.length === 0) {
        return null;
    }

    return (
        <div className={classes.root}>
            {
                manufacturers.map((item, i) => <Item item={item} key={`${item.name}-${i}`} />)
            }
        </div>
    )
}