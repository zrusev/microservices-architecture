import React, { useState, useEffect } from 'react';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';
import useStyles from '../../style/shared/snackbar';

const Alert = (props) => (<MuiAlert elevation={6} variant="filled" {...props} />);

export const CustomSnackbar = ({message, severity}) => {
    const classes = useStyles();
    const [open, setOpen] = useState(false);

    useEffect(() => {
        setOpen(true);
    },[message])

    const handleClose = (event, reason) => {
        setOpen(false);
    };

    return (
        <div className={classes.root}>
            <Snackbar
                open={open}
                autoHideDuration={2500}
                onClose={handleClose}
                anchorOrigin={{vertical: 'top', horizontal: 'center'}}
            >
                <Alert onClose={handleClose} severity={severity}>
                    {message}
                </Alert>
            </Snackbar>
        </div>
    )
}