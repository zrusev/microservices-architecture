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
    const {page, category, manufacturer, name} = params;

    const [pageNumber, setPageNumber] = useState(parseInt(page) || 1);

    const cards = useSelector(state => state.products.products);

    useEffect(() => {
        dispatch(productsActions.get(parseInt(page || 1),
            category,
            manufacturer,
            name));
    }, [dispatch, page, category, manufacturer, name]);

    const handleChange = (event, page) => {
        history.push({
            pathname: '/products',
            search: ((page || '') && `?page=${page}`) +
                    ((category || '') && `&category=${category}`) +
                    ((manufacturer || '') && `&manufacturer=${manufacturer}`) +
                    ((name || '') && `&name=${name}`)
        });

        setPageNumber(page);
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