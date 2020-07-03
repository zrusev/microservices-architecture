import React from 'react';
import { useSelector } from 'react-redux';
import Header from '../../components/Landing/Header';
import Footer from '../../components/Landing/Footer';
import Main from '../../components/Landing/Main';

export const HomePage = () => {
    const loggingIn = useSelector(state => state.authentication.loggingIn);

    if (loggingIn)
      return <div className="col-sm-8 col-sm-offset-2">Logging now.....</div>
    
    return (
      <React.Fragment>
        <Header />
          <Main />
        <Footer />
    </React.Fragment>
    )
}