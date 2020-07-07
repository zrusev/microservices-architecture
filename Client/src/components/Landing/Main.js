import React, { useContext, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { customerGatewayActions } from '../../+store/actions';
import { NavLink } from 'react-router-dom';
import { ThemeContext } from '../../style/contexts/EnhancedThemeProvider';
import {
  Button,
  Grid,
  Typography,
  Container,
  Link,
} from '@material-ui/core';
import { CarouselSlider, Category, Manufacturer } from '../index';
import useStyles from '../../style/Landing/main';

export const Main = () => {
    const classes = useStyles();
    const dispatch = useDispatch();

    const { darkState } = useContext(ThemeContext);
    const topProducts = useSelector(state => state.topProducts.products);

    useEffect(() => {
      dispatch(customerGatewayActions.topProducts());
    }, [dispatch]);

    if(topProducts.length === 0) {
      return null;
    }

    return (
        <React.Fragment>
          <div className={darkState ? classes.darkContent : classes.heroContent} style={{minHeight: '90vh'}}>
            <Container maxWidth="sm">
              <Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom>
                MY STORE
              </Typography>
              <Typography variant="h5" align="center" color="textSecondary" paragraph>
                Search & Buy Any Product Online
              </Typography>
              <div className={classes.heroButtons}>
                <Grid container spacing={1} justify="center">
                  <Grid item>
                    <Link component={NavLink} to="/products/pages/1">
                      <Button variant="contained" size="large" color="primary">
                        Explore
                      </Button>
                    </Link>
                  </Grid>
                </Grid>
              </div>
            </Container>

            <Container>
              <Grid container spacing={1} justify="center" height="100%">
                <Grid item style={{marginTop: '5em'}}>
                  <Typography component="h4" variant="h5" align="center" color="textPrimary" gutterBottom>
                  Best Selling Products Per Category
                  </Typography>
                </Grid>
                <Grid item>
                  <CarouselSlider items={topProducts} />
                </Grid>
              </Grid>
            </Container>
          </div>

          <Container className={classes.cardGrid} maxWidth="md" style={{minHeight: '80vh'}}>
            <Grid container spacing={1} justify="center" height="100%">
                <Grid item style={{marginTop: '2em'}}>
                  <Typography component="h4" variant="h5" align="center" color="textPrimary" gutterBottom>
                  What Are You Looking For?
                  </Typography>
                </Grid>
                <Grid item>
                  <Category />
                </Grid>
            </Grid>
          </Container>

          <div className={darkState ? classes.darkContent : classes.heroContentBottom} style={{minHeight: '80vh'}}>
            <Container className={classes.cardGrid}>
              <Grid container spacing={1} justify="center" height="100%">
                  <Grid item style={{marginTop: '2em'}}>
                    <Typography component="h4" variant="h5" align="center" color="textPrimary" gutterBottom>
                    Look At Our Top Manufacturers
                    </Typography>
                  </Grid>
                  <Grid item>
                    <Manufacturer />
                  </Grid>
              </Grid>
            </Container>
          </div>
      </React.Fragment>
    )
}