import { combineReducers } from 'redux';
import { authentication } from './authentication.reducer';
import { customer } from './customer.reducer';

import { alert } from './alert.reducer';

const rootReducer = combineReducers({
    authentication,
    customer,
    alert
  });

export default rootReducer;