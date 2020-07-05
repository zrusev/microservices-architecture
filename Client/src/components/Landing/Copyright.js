import React from 'react';
import { Typography } from '@material-ui/core';

export const Copyright = () => {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {'Copyright © '}
      My Store 2019 -
      {' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}