import React, { useEffect } from 'react';
import { Router, Switch, Route, Redirect } from 'react-router-dom';
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
import { Details } from './views/Products/Details';
import { CustomSnackbar } from './components/index';

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
            <div className={`alert ${Handler(alert).type}`}>
              <CustomSnackbar message={Handler(alert).message} severity={Handler(alert).severity} />
            </div>
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
                  <Route path="/products/details/:id/:name" component={Details} />
                  <Route path="/products" component={Products} />
                  <ProtectedRoute path="/profile" component={Profile} />
                  <Route render={() => <Redirect to="/" />} />
                </Switch>
              </main>
            <Footer />
          </Router>
        </EnhancedThemeProvider>
    </React.Fragment>
  );
}

export default App;