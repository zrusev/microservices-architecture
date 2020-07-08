import React from 'react';
import {
    Grid,
} from '@material-ui/core';
import { ProductCard } from './ProductCard';

export const ProductCards = ({ cards: { products }}) => {
   return (
        <Grid container spacing={4}>
            {products.map((card, i) => (
                <Grid item key={`item-${i}`} xs={12} sm={6} md={4}>
                    <ProductCard card={card} activeLink={true} />
                </Grid>
            ))}
        </Grid>
    )
}