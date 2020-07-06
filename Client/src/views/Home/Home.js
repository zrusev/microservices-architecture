import React from 'react';
import { useSelector } from 'react-redux';
import { Main } from '../../components/index';

export const HomePage = () => {
    const loggingIn = useSelector(state => state.authentication.loggingIn);

    if (loggingIn) {
      return <div style={{minHeight: '90vh'}}>Logging now...</div>
    }

    return <Main />
}