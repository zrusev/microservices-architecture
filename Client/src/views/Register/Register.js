import React, { useState, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { identityActions } from '../../+store/actions';
import { history } from '../../helpers';
import Register from '../../components/Register/Register';

export const RegisterPage = props => {
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
        dispatch(identityActions.register(email, password));

        history.push("/");
      }
    };
    
    const { email, password } = state;
     
    return (
      <div style={{minHeight: '90vh'}}>
        <Register 
          email={email} 
          password={password} 
          handleInputChange={handleInputChange} 
          handleSubmit={handleSubmit} />
      </div>
    );
}