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
import LabelIcon from '@material-ui/icons/Label';
import useStyles from '../../style/Landing/drawer';

export const SidePanel = ({toggleDrawer, open}) => {
    const classes = useStyles();
    const categories = useSelector(state => state.categories.categories);

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
                  component={NavLink}
                  to={{
                    pathname: '/products',
                    search: `?page=${1}&category=${item.name.replace(/\s/g, '-').toLowerCase()}`}
                  }
                >
                  <ListItem button key={`${item.name}-${index}`}>
                    <ListItemIcon>{index % 2 === 0 ? <CategoryIcon /> : <LabelIcon />}</ListItemIcon>
                    <ListItemText primary={item.name} />
                  </ListItem>
                </Link>
              ))
            }
          </List>
          <Divider />
          <List>
            {['About', 'Contacts'].map((text, index) => (
              <ListItem button key={text}>
                <ListItemIcon>{index % 2 === 0 ? <CategoryIcon /> : <LabelIcon />}</ListItemIcon>
                <ListItemText primary={text} />
              </ListItem>
            ))}
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