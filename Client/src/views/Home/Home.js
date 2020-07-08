import React from 'react';
import { useSelector } from 'react-redux';
import { Main } from '../../components/index';
import { Spinner } from '../../components/index';

export const HomePage = () => {
    const loggingIn = useSelector(state => state.authentication.loggingIn);

    if (loggingIn) {
      return <Spinner />
    }

    return <Main />
}