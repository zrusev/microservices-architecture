import React from 'react';
import Typography from '@material-ui/core/Typography';
import Copyright from './Copyright';
import useStyles from '../../style/Home/style';

export default function Footer() {
    const classes = useStyles();

    return (
        <footer className={classes.footer}>
            <Typography variant="subtitle1" align="center" color="textSecondary" component="p">
                Modularity - Scalability - Integration - Distributed development
            </Typography>
            <Copyright />
      </footer>
    )
}