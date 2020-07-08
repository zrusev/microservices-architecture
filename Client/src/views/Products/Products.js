import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { productsActions } from '../../+store/actions';
import { ProductCards } from '../../components/index';
import {
    Container,
} from '@material-ui/core';
import Pagination from '@material-ui/lab/Pagination';
import { Spinner } from '../../components/index';
import useStyles from '../../style/Products/products';
import { history } from '../../helpers';
import queryString from 'query-string';

export const Products = (props) => {
    const classes = useStyles();
    const dispatch = useDispatch();

    const produtsPerPage = 10;

    const url = props.location.search;
    const params = queryString.parse(url);
    const {page, category, manufacturer} = params;

    const [pageNumber, setPageNumber] = useState(parseInt(page) || 1);
    const [categoryFilter] = useState(category);
    const [manufacturerFilter] = useState(manufacturer);

    const cards = useSelector(state => state.products.products);

    useEffect(() => {
        dispatch(productsActions.get(pageNumber, categoryFilter, manufacturerFilter));
    }, [dispatch, pageNumber, categoryFilter, manufacturerFilter]);

    const handleChange = (event, page) => {
        setPageNumber(page);

        history.push({
            pathname: '/products',
            search: ((page || '') && `?page=${page}`) +
                    ((categoryFilter || '') && `&category=${categoryFilter}`) +
                    ((manufacturerFilter || '') && `&manufacturer=${manufacturerFilter}`)
        });
    };

    if(cards.length === 0) {
        return <Spinner />
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