import React from 'react';
import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import useStyles from '../../style/Home/drawer';

export default function SidePanel({toggleDrawer, open}) {
    const classes = useStyles();
    
    const list = (anchor) => (
        <div className={classes.list}
          role="presentation"
          onClick={toggleDrawer(false)}
          onKeyDown={toggleDrawer(false)}
        >
          <List>
            {['Category1', 'Category2', 'Category3', 'Category4'].map((text, index) => (
              <ListItem button key={text}>
                <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
                <ListItemText primary={text} />
              </ListItem>
            ))}
          </List>
          <Divider />
          <List>
            {['About', 'Contacts'].map((text, index) => (
              <ListItem button key={text}>
                <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
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