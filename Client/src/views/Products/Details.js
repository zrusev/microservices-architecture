import React, { useEffect } from 'react';
import { NavLink } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { productsActions } from '../../+store/actions';
import { ProductCard } from '../../components/index';
import {
    Container,
    Grid,
    Link,
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

    const nameFilter = params.name.replace(/\s/g, '%20').toLowerCase();
    const idFilter = params.id;

    useEffect(() => {
        debugger
        dispatch(productsActions.getProduct(idFilter, nameFilter));
    }, [dispatch, nameFilter, idFilter]);

    if(!card) {
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
                                <Link className={classes.addToBasket} component={NavLink} to="/" color="textPrimary">
                                    <Button size="small" color="primary" variant="contained">
                                        ADD TO BASKET
                                    </Button>
                                </Link>
                            </CardActions>
                        </Card>
                    </Grid>
                </Grid>
            </Container>
        </div>
    )
}