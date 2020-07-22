import { combineReducers } from 'redux';
import { alert } from './alert.reducer';
import { notification } from './notification.reducer';
import { authentication } from './authentication.reducer';
import { customer } from './customer.reducer';
import { products } from './products.reducer';
import { topProducts } from './customer.gateway.reducer';
import { categories } from './categories.reducer';
import { manufacturers } from './manufacturers.reducer';
import { statistics } from './statistics.reducer';
import { basket } from './basket.reducer';

const rootReducer = combineReducers({
    alert,
    notification,
    authentication,
    customer,
    products,
    topProducts,
    categories,
    manufacturers,
    statistics,
    basket,
  });

export default rootReducer;