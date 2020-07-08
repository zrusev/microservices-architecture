import React from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import useStyles from '../../style/shared/spinner';

export const Spinner = () => {
  const classes = useStyles();

  return (
    <div className={classes.root} >
      <CircularProgress
        className={classes.spinner}
        color="secondary"
        size={70}
    />
    </div>
  );
}
