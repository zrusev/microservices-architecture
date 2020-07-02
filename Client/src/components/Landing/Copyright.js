import React from 'react';
import { NavLink } from 'react-router-dom';
import Typography from '@material-ui/core/Typography';

export default function Copyright() {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {'Copyright © '}
      <NavLink color="inherit" to="/">My Store</NavLink>
      {' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}