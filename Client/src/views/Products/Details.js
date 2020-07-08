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
} from '@material-ui/core';
import { Spinner } from '../../components/index';
import useStyles from '../../style/Landing/main';

export const Details = ({ match: { params } }) => {
    const classes = useStyles();

    const dispatch = useDispatch();
    const card = useSelector(state => state.products.product);

    const filter = params.name;

    useEffect(() => {
        dispatch(productsActions.getProduct(filter));
    }, [dispatch, filter]);

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
                            <CardActions>
                                <Link component={NavLink} to="/" color="textPrimary">
                                    <Button size="small" color="primary">
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