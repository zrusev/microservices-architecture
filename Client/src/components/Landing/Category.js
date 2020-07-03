import React from 'react';
import { NavLink } from 'react-router-dom';
import Link from '@material-ui/core/Link';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import CategoryIcon from '@material-ui/icons/Category';
import useStyles from '../../style/Home/category';

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

export default function Category() {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            {
                categories.map((c, i) => <Item item={c} key={`${c}-${i}`} />)
            }
        </div>
    )
}