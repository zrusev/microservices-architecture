import React, { useEffect } from 'react';
import { Router, Switch, Route } from 'react-router-dom';
import CssBaseline from '@material-ui/core/CssBaseline';
import {
  HomePage,
  LoginPage,
  RegisterPage,
  Profile,
  Products,
} from './views';
import { ProtectedRoute } from './components/ProtectedRoute/ProtectedRoute';
import { history } from './helpers';
import { useSelector, useDispatch } from 'react-redux';
import { alertActions } from './+store/actions';
import EnhancedThemeProvider from './style/contexts/EnhancedThemeProvider';
import {
  Header,
  Footer,
} from './components/index';
import Handler from './helpers/error';

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
            <div className={`alert ${Handler(alert).type}`}>{Handler(alert).message}</div>
          }
        </div>
        <CssBaseline />
        <EnhancedThemeProvider>
          <Router history={history}>
            <Header />
              <main>
                <Switch>
                  <Route exact path="/" component={HomePage} />
                  <Route path="/login" component={LoginPage} />
                  <Route path="/register" component={RegisterPage} />
                  <Route path="/products/pages/:page" component={Products} />
                  <ProtectedRoute path="/profile" component={Profile} />
                </Switch>
              </main>
            <Footer />
          </Router>
        </EnhancedThemeProvider>
    </React.Fragment>
  );
}

export default App;