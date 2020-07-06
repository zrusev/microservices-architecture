import React from 'react';
import parse from 'html-react-parser';
import {
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Grid,
    Typography,
} from '@material-ui/core';
import useStyles from '../../style/Landing/main';

export const ProductCard = ({ cards: { products }}) => {
    const classes = useStyles();
    const handleImageError = (e) => {
        debugger
        e.target.onerror = null;
        e.target.src = "https://via.placeholder.com/150";
    }

    return (
        <Grid container spacing={4}>
            {products.map((card, i) => (
                <Grid item key={i} xs={12} sm={6} md={4}>
                    <Card className={classes.card}>
                    <CardMedia
                        className={classes.cardMedia}
                        image={card.image_url}
                        title={`${card.name}-${i}`}
                        onError={handleImageError}
                    />
                    <CardContent className={classes.cardContent}>
                        <Typography gutterBottom variant="h5" component="div">
                            {card.name}
                        </Typography>
                        <Typography className={classes.cardProduct_description} component="div">
                            {parse(card.description)}
                        </Typography>
                    </CardContent>
                    <CardActions>
                        <Button size="small" color="primary">
                            View
                        </Button>
                        <Button size="small" color="primary">
                            Edit
                        </Button>
                    </CardActions>
                    </Card>
                </Grid>
            ))}
        </Grid>
    )
}