import React, { useState, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { identityActions } from '../../+store/actions';
import { history } from '../../helpers';
import { Facebook } from '../../components/Login/External/Facebook';
import Login from '../../components/Login/Login';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';

export const LoginPage = () => {
    const dispatch = useDispatch();
    
    const initialState = { email: '', password: ''};
    const [state, setState] = useState(initialState);
    
    useEffect(() => {
      dispatch(identityActions.logout());
    }, [dispatch]);

    const handleInputChange = event => {
        event.persist();
        
        setState(inputs => ({
            ...inputs, 
            [event.target.name]: event.target.value 
        }));
    };
    
    const handleSubmit = event => {
        if (event) {
            event.preventDefault();
        }

        const { email, password } = state;

        if (email && password) {
          dispatch(identityActions.login(email, password));

          history.push("/");
        }
    }

    const { email, password } = state;

    return (
      <div style={{minHeight: '90vh'}}>
            <Container maxWidth="sm">
                <Grid container spacing={1} justify="center">
                  <Grid item>
                    <Login
                      email={email} 
                      password={password} 
                      handleInputChange={handleInputChange} 
                      handleSubmit={handleSubmit} />
                  </Grid>
                  <Grid item>
                    <Facebook />
                  </Grid>
                </Grid>
            </Container>
      </div>
    );
}