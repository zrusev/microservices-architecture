import React from 'react';
import Carousel from 'react-material-ui-carousel'
import {
    Card,
    CardMedia,
    CardActionArea,
    Grid,
} from '@material-ui/core';
import useStyles from '../../style/Home/carouselSlider';

const Item = ({itemsSet}) => {
    const classes = useStyles();
    const totalItems = itemsSet.length ? itemsSet.length : 3;

    return (
        <Card raised={true} className={classes.card}>
            <Grid container length={12} spacing={1} justify="center">
            {
                itemsSet.map((item, i) => 
                    <Grid item md={12 / totalItems} key={`${item.Name}-${i}`}>
                        <CardActionArea>
                            <CardMedia
                                className={classes.media}
                                image={item.Image}
                                title={item.Name}
                            />
                            <div className={classes.overlay} color="textPrimary">
                                {item.Name}
                            </div>
                        </CardActionArea>
                    </Grid>
                )    
            }         
            </Grid>
        </Card>
    )
}

export default function CarouselSlider({items}) {
    return (
        <Carousel
            className="Example"
            autoPlay={true}
            interval={5000}
            animation={"fade"}
            indicators={false}
            timeout={500}
            navButtonsAlwaysVisible={false}
        >
            {
                items.map((item, i) => 
                    <Item key={`${item.Name}-${i}`} itemsSet={item.Items} /> )
            }
        </Carousel>
    );
}