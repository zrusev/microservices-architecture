import React from 'react';
import { NavLink } from 'react-router-dom';
import {
    Link,
    Paper,
    Typography,
    Divider,
} from '@material-ui/core';
import CategoryIcon from '@material-ui/icons/Category';
import useStyles from '../../style/Landing/category';

const categories = [
    'За дома'
    ,'Тяло'
    ,'Серуми за лице'
    ,'Benecos'
    ,'Тоник за лице'
    ,'Суха коса'
    ,'Percy Nobleman'
    ,'Apiarium'
    ,'Dr.Craft'
]

const Item = ({item}) => {
    const classes = useStyles();

    return (
        <Link component={NavLink} to="/" className={classes.item}>
            <Paper elevation={3} className={classes.paper}>
                <CategoryIcon className={classes.icon} />
                <Divider />
                <Typography
                    className={classes.title}
                    variant="h6"
                    noWrap>
                    {item}
                </Typography>
            </Paper>
        </Link>
    )
}

export const Category = () => {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            {
                categories.map((c, i) => <Item item={c} key={`${c}-${i}`} />)
            }
        </div>
    )
}