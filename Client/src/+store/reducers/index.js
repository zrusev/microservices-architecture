import { combineReducers } from 'redux';
import { authentication } from './authentication.reducer';
import { customer } from './customer.reducer';
import { products } from './products.reducer';
import { topProducts } from './customer.gateway.reducer';
import { categories } from './categories.reducer';
import { manufacturers } from './manufacturers.reducer';

import { alert } from './alert.reducer';

const rootReducer = combineReducers({
    authentication,
    customer,
    alert,
    products,
    topProducts,
    categories,
    manufacturers,
  });

export default rootReducer;