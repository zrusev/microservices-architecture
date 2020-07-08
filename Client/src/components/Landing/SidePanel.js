import React from 'react';
import { NavLink } from 'react-router-dom';
import { useSelector } from 'react-redux';
import {
  Drawer,
  List,
  Divider,
  ListItem,
  ListItemIcon,
  ListItemText,
  Link,
} from '@material-ui/core';
import CategoryIcon from '@material-ui/icons/Category';
import LocationCityIcon from '@material-ui/icons/LocationCity';
import useStyles from '../../style/Landing/drawer';

export const SidePanel = ({toggleDrawer, open}) => {
    const classes = useStyles();

    const categories = useSelector(state => state.categories.categories);
    const manufacturers = useSelector(state => state.manufacturers.manufacturers);

    const list = (anchor) => (
        <div className={classes.list}
          role="presentation"
          onClick={toggleDrawer(false)}
          onKeyDown={toggleDrawer(false)}
        >
          <List>
            {
              categories.map((item, index) => (
                <Link
                  key={`${item.name}-${index}`}
                  component={NavLink}
                  to={{
                    pathname: '/products',
                    search: `?page=${1}&category=${item.name.replace(/\s/g, '-').toLowerCase()}`}
                  }
                >
                  <ListItem button>
                    <ListItemIcon>
                      <CategoryIcon />
                    </ListItemIcon>
                    <ListItemText primary={item.name} />
                  </ListItem>
                </Link>
              ))
            }
          </List>
          <Divider />
          <List>
            {
              manufacturers.slice(0, 5).map((item, index) => (
                <Link
                  key={`${item.name}-${index}`}
                  component={NavLink}
                  to={{
                    pathname: '/products',
                    search: `?page=${1}&category=${item.name.replace(/\s/g, '-').toLowerCase()}`}
                  }
                >
                  <ListItem button>
                    <ListItemIcon>
                      <LocationCityIcon />
                    </ListItemIcon>
                    <ListItemText primary={item.name} />
                  </ListItem>
                </Link>
              ))
            }
          </List>
        </div>
    );

    return (
        <div> {['left'].map((anchor) => (
            <React.Fragment key={anchor}>
                <Drawer anchor={anchor} open={open} onClose={toggleDrawer(false)}>
                {list(anchor)}
                </Drawer>
            </React.Fragment>
            ))}
        </div>
    );
}