import React from 'react';
import { NavLink } from 'react-router-dom';
import parse from 'html-react-parser';
import {
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Typography,
    Link,
} from '@material-ui/core';
import useStyles from '../../style/Landing/main';

export const ProductCard = ({card, activeLink}) => {
    const classes = useStyles();

    return (
        <Card className={classes.card}>
            {
                activeLink ?
                <Link component={NavLink} to={`/products/details/${card.id}/${card.name.replace(/\s/g, '-').toLowerCase()}`} color="textPrimary">
                    <CardMedia
                        className={classes.cardMedia}
                        image="https://source.unsplash.com/random"
                        title={`${card.name}-${card.id}`}
                    />
                </Link> :
                <CardMedia
                    className={classes.cardMedia}
                    image={card.image_url}
                    title={`${card.name}-${card.id}`}
                />
            }
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
    )
}