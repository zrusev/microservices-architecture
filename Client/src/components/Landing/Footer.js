import React from 'react';
import { Typography } from '@material-ui/core';
import { Copyright } from '../index';
import useStyles from '../../style/Home/main';

export const Footer = () => {
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