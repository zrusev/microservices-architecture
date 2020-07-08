import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { productsActions } from '../../+store/actions';
import { ProductCards } from '../../components/index';
import {
    Container,
} from '@material-ui/core';
import Pagination from '@material-ui/lab/Pagination';
import useStyles from '../../style/Products/products';
import { history } from '../../helpers';

export const Products = ({ match: { params } }) => {
    const classes = useStyles();
    const dispatch = useDispatch();
    const produtsPerPage = 10;

    const [pageNumber, setPageNumber] = useState(parseInt(params.page) || 1);
    const cards = useSelector(state => state.products.products);

    useEffect(() => {
        dispatch(productsActions.get(pageNumber));
    }, [dispatch, pageNumber]);

    const handleChange = (event, value) => {
      setPageNumber(value);
      history.push(`/products/pages/${value}`);
    };

    if(cards.length === 0) {
        return null;
    }

    return (
        <div style={{minHeight: '90vh', margin: '2em'}}>
            <Container maxWidth="lg">
                <ProductCards cards={cards} />
            </Container>
            <Container maxWidth="lg">
                <Pagination
                    className={classes.root}
                    count={Math.ceil(cards.totalProducts/produtsPerPage)}
                    page={pageNumber}
                    onChange={handleChange}
                />
            </ Container>
        </div>
    )
}