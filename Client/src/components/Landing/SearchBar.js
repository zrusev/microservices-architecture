import React, { useState } from 'react';
import SearchIcon from '@material-ui/icons/Search';
import { InputBase } from '@material-ui/core';
import { history } from '../../helpers';
import useStyles from '../../style/Landing/search';

export const SearchBar = () => {
    const classes = useStyles();

    const [filter, setFilter] = useState('');

    const params = {
        page: 1,
        name: filter.replace(/\s/g, '-'),
    }

    const handleChange = (event) => {
        setFilter(event.target.value);
    };

    const handleKeyPress = (event) => {
        if (event.key === 'Enter') {

            history.push({
                pathname: '/products',
                search: ((params.page || '') && `?page=${params.page}`) +
                ((params.name || '') && `&name=${params.name}`)
            });
          }
    }

    return (
        <div className={classes.search}>
            <div className={classes.searchIcon}>
            <SearchIcon />
            </div>
            <InputBase
                placeholder="Search…"
                classes={{
                    root: classes.inputRoot,
                    input: classes.inputInput,
                }}
                inputProps={{ 'aria-label': 'search' }}
                onChange={handleChange}
                onKeyPress={handleKeyPress}
            />
      </div>
    )
}