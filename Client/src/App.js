import React, { useEffect } from 'react';
import { Router, Switch, Route } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import { HomePage, LoginPage, RegisterPage, DashboardPage } from './views';
import { ProtectedRoute } from './components/ProtectedRoute/ProtectedRoute';
import { history } from './helpers';
import { useSelector, useDispatch } from 'react-redux';
import { alertActions } from './+store/actions';
import EnhancedThemeProvider from './style/contexts/EnhancedThemeProvider';

const App = () => {
  const alert = useSelector(state => state.alert);
  const dispatch = useDispatch();

  useEffect(() => {
    history.listen((location, action) => {
      dispatch(alertActions.clear());
    });
  }, [dispatch]);


  return (
    <React.Fragment>
        <div>
          {
            alert.message && 
            alert.message.map(message => <div key={message.code} className={`alert ${alert.type}`}>{message.description}</div>)
          }
        </div>
        <CssBaseline />
        <EnhancedThemeProvider>
          <Router history={history}>
            {/* <Navigation /> */}
            <Switch>
              <Route exact path="/" component={HomePage} />
              <Route path="/login" component={LoginPage} />
              <Route path="/register" component={RegisterPage} />
              <ProtectedRoute path="/dashboard" component={DashboardPage} />
            </Switch>
          </Router>
        </EnhancedThemeProvider>
    </React.Fragment>
  );
}

export default App;