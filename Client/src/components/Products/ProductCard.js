import React from 'react';
import { NavLink } from 'react-router-dom';
import parse from 'html-react-parser';
import {
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Grid,
    Typography,
    Link,
} from '@material-ui/core';
import useStyles from '../../style/Landing/main';

export const ProductCard = ({ cards: { products }}) => {
    const classes = useStyles();

    return (
        <Grid container spacing={4}>
            {products.map((card, i) => (
                <Grid item key={i} xs={12} sm={6} md={4}>
                    <Card className={classes.card}>
                    <Link component={NavLink} to={`/products?name=${card.name.replace(/\s/g, '-').toLowerCase()}`} color="textPrimary">
                        <CardMedia
                            className={classes.cardMedia}
                            image={card.image_url}
                            title={`${card.name}-${i}`}
                        />
                    </Link>
                    <CardContent className={classes.cardContent}>
                        <Typography gutterBottom variant="h5" component="div">
                            {card.name}
                        </Typography>
                        <div style={{opacity: '1'}}>
                        <Typography className={classes.cardProduct_description} component="div">
                            {parse(card.description)}
                        </Typography>
                        </div>
                    </CardContent>
                    <CardActions>
                        <Link target="_blank" component={NavLink} to={`/${card.url.replace(/(^.+?\/)/g,'')}`} color="textPrimary">
                            <Button size="small" color="primary">
                                Link
                            </Button>
                        </Link>
                    </CardActions>
                    </Card>
                </Grid>
            ))}
        </Grid>
    )
}