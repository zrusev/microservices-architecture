import { combineReducers } from 'redux';
import { authentication } from './authentication.reducer';
import { customerCreation } from './customer.reducer';

import { alert } from './alert.reducer';

const rootReducer = combineReducers({
    authentication,
    customerCreation,
    alert
  });

export default rootReducer;