import React, { useState } from 'react';
import AppBar from '@material-ui/core/AppBar';
import MenuIcon from '@material-ui/icons/Menu';
import IconButton from '@material-ui/core/IconButton';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import SidePanel from './SidePanel';
import SearchBar from './SearchBar';
import { NavLink } from 'react-router-dom';
import Link from '@material-ui/core/Link';
import useStyles from '../../style/Home/header';

export default function Header() {
    const classes = useStyles();
    
    const [open, setOpen] = useState(false);
  
    const toggleDrawer = (open) => (event) => {
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
          return;
        }
  
        setOpen(open);
    };

    const LinkBehavior = React.forwardRef((props, ref) => (
        <NavLink ref={ref} to="/" {...props} />
    ));

    return (
        <React.Fragment>
            <SidePanel toggleDrawer={toggleDrawer} open={open} />
            <CssBaseline />
            <div className={classes.grow}>
                <AppBar position="static">
                <Toolbar>
                    <IconButton
                        onClick={toggleDrawer(true)}
                        edge="start"
                        className={classes.menuButton}
                        color="inherit"
                        aria-label="open drawer"
                    >
                        <MenuIcon 
                            style={{color: "white"}} 
                            className={classes.icon} />
                    </IconButton>
                    <Link component={LinkBehavior}>
                        <Typography 
                            className={classes.title} 
                            variant="h6" 
                            noWrap>
                            Logo
                        </Typography>
                    </Link>
                    <SearchBar />
                </Toolbar>
                </AppBar>
            </div>
        </React.Fragment>
    )
}