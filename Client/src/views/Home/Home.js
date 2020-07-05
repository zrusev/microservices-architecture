import React from 'react';
import { useSelector } from 'react-redux';
import { Main } from '../../components/index';

export const HomePage = () => {
    const loggingIn = useSelector(state => state.authentication.loggingIn);

    if (loggingIn)
      return <div className="col-sm-8 col-sm-offset-2">Logging now.....</div>

    return (
      <Main />
    )
}