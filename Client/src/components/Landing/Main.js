import React, { useContext } from 'react';
import { ThemeContext } from '../../style/contexts/EnhancedThemeProvider';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import CarouselSlider from './CarouselSlider';
import Category from './Category';
import Manufacturer from './Manufacturer';
import useStyles from '../../style/Home/style';

export default function Main() {
    const classes = useStyles();
    const { darkState } = useContext(ThemeContext);

    const items = [
    {
        Name: "Home Appliances",
        Caption: "Say no to manual home labour!",
        contentPosition: "middle",
        Items: [
            {
                Name: "Macbook Pro",
                Image: "https://source.unsplash.com/featured/?macbook"
            },
            {
                Name: "Washing Machine WX9102",
                Image: "https://source.unsplash.com/featured/?washingmachine"
            },
            {
                Name: "Learus Vacuum Cleaner",
                Image: "https://source.unsplash.com/featured/?vacuum,cleaner"
            },
            {
              Name: "Washing Machine WX9102",
              Image: "https://source.unsplash.com/featured/?washingmachine"
            },
        ]
    },
    {
        Name: "Decoratives",
        Caption: "Give style and color to your living room!",
        contentPosition: "right",
        Items: [
            {
                Name: "iPhone",
                Image: "https://source.unsplash.com/featured/?iphone"
            },
            {
                Name: "Living Room Lamp",
                Image: "https://source.unsplash.com/featured/?lamp"
            },
            {
                Name: "Floral Vase",
                Image: "https://source.unsplash.com/featured/?vase"
            },
            {
              Name: "Living Room Lamp",
              Image: "https://source.unsplash.com/featured/?lamp"
            },
        ]
    }
  ]

    return (
        <main>
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
                    <Button variant="contained" size="large" color="primary">
                      Explore
                    </Button>
                  </Grid>
                </Grid>
              </div>
            </Container>

            <Container>
              <Grid container spacing={1} justify="center" height="100%">
                <Grid item style={{marginTop: '2em'}}>
                  <Typography component="h4" variant="h5" align="center" color="textPrimary" gutterBottom>
                    BEST SELLING PRODUCTS:
                  </Typography>              
                </Grid>
                <Grid item>
                  <CarouselSlider items={items} />
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
      </main>
    )
}