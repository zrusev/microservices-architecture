import React from 'react';
import { NavLink } from 'react-router-dom';
import Link from '@material-ui/core/Link';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import LocationCityIcon from '@material-ui/icons/LocationCity';
import useStyles from '../../style/Home/manufacturer';

const manufacturers = [
    'Laidbare'
    ,'Olival'
    ,'Cosnature'
    ,'Radico'
    ,'Organic Shop'
    ,'Hydrophil'
    ,'Humble Brush'
    ,'Biopark Cosmetics'
    ,'Khadi'
    ,'Lunay'
    ,'Afterspa'
]

const Item = ({item}) => {
    const classes = useStyles();

    return (
        <Link component={NavLink} to="/" className={classes.item}>
            <Paper elevation={3} className={classes.paper}>
                <LocationCityIcon className={classes.icon} />
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

export default function Manufacturer() {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            {
                manufacturers.map((m, i) => <Item item={m} key={`${m}-${i}`} />)
            }
        </div>
    )
}