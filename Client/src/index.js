import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { store } from './+store/store';

import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';

console.log(`Currently working in ${process.env.REACT_APP_ENV || 'local'} environment.`);

ReactDOM.render(
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('root')
);

serviceWorker.unregister();