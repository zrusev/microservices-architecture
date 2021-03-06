import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { categoriesActions } from '../../+store/actions';
import { NavLink } from 'react-router-dom';
import {
    Link,
    Paper,
    Typography,
    Divider,
} from '@material-ui/core';
import CategoryIcon from '@material-ui/icons/Category';
import useStyles from '../../style/Landing/category';

const Item = ({item}) => {
    const classes = useStyles();
    const params = {
        page: 1,
        category: item.name,
    }

    return (
        <Link
            className={classes.item}
            component={NavLink}
            to={{
                pathname: '/products',
                search: `?page=${params.page}&category=${params.category.toLowerCase()}`,
            }}
        >
            <Paper elevation={3} className={classes.paper}>
                <CategoryIcon className={classes.icon} />
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

export const Category = () => {
    const classes = useStyles();

    const dispatch = useDispatch();
    const categories = useSelector(state => state.categories.categories);

    useEffect(() => {
        dispatch(categoriesActions.topCategories());
    }, [dispatch]);

    if(categories.length === 0) {
        return null;
    }

    return (
        <div className={classes.root}>
            {
                categories.map((c, i) => <Item item={c} key={`${c}-${i}`} />)
            }
        </div>
    )
}