import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { productsActions, basketActions } from '../../+store/actions';
import { ProductCard } from '../../components/index';
import {
    Container,
    Grid,
    Card,
    CardActions,
    Button,
    Typography,
} from '@material-ui/core';
import { Spinner } from '../../components/index';
import useStyles from '../../style/Landing/main';

export const Details = ({ match: { params } }) => {
    const classes = useStyles();

    const dispatch = useDispatch();
    const card = useSelector(state => state.products.product);
    const visits = useSelector(state => state.statistics.seenProduct);

    const nameFilter = params.name;
    const idFilter = params.id;

    useEffect(() => {
        dispatch(productsActions.getProduct(idFilter, nameFilter));
    }, [dispatch, nameFilter, idFilter]);

    const handleAddClick = () => {
        dispatch(basketActions.addToBasket({name: nameFilter, id: idFilter}));
    };

    if(!card && visits !== 0) {
        return <Spinner />
    }

    return (
        <div style={{minHeight: '90vh', margin: '2em'}}>
            <Container maxWidth="lg">
                <Grid container spacing={4}>
                    <Grid item xs={12} sm={6} md={6}>
                        <ProductCard card={card} style={{ '&:hover': '' }}/>
                    </Grid>
                    <Grid item xs={12} sm={6} md={6}>
                        <Card className={classes.card}>
                            <Typography className={classes.seen} align="center" component="h5" color="textSecondary">
                                Seen: {visits && visits.totalVisits} times
                            </Typography>
                            <CardActions>
                                <Button
                                    size="small"
                                    color="primary"
                                    variant="contained"
                                    className={classes.addToBasket}
                                    onClick={handleAddClick}
                                >
                                    ADD TO BASKET
                                </Button>
                            </CardActions>
                        </Card>
                    </Grid>
                </Grid>
            </Container>
        </div>
    )
}