import React from 'react';
import { NavLink } from 'react-router-dom';
import {
    Card,
    CardMedia,
    CardActionArea,
    Grid,
    Link,
} from '@material-ui/core';
import Carousel from 'react-material-ui-carousel'
import useStyles from '../../style/Landing/carouselSlider';

const Item = ({itemsSet}) => {
    const classes = useStyles();
    const totalItems = itemsSet.length ? itemsSet.length : 3;

    return (
        <Card raised={true} className={classes.card}>
            <Grid container length={12} spacing={1} justify="center">
            {
                itemsSet.map((item, i) =>
                    <Grid item md={12 / totalItems} key={`${item.name}-${i}`}>
                        <Link component={NavLink} to={`/products/details/${item.id}/${item.name}`} color="textPrimary">
                            <CardActionArea>
                                <CardMedia
                                    className={classes.media}
                                    image={item.image_url}
                                    title={item.name}
                                />
                                <div className={classes.overlay}>
                                    {item.name}
                                </div>
                            </CardActionArea>
                        </Link>
                    </Grid>
                )
            }
            </Grid>
        </Card>
    )
}

export const CarouselSlider = ({items}) => {
    return (
        <Carousel
            className="Example"
            autoPlay={true}
            interval={3000}
            animation={"fade"}
            indicators={false}
            timeout={500}
            navButtonsAlwaysVisible={false}
        >
            {
                items.map((item, i) =>
                    <Item key={`${item.category}-${i}`} itemsSet={item.products} /> )
            }
        </Carousel>
    );
}