import React from 'react';
import { Provider } from 'react-redux';
import { Router, hashHistory, Route, IndexRoute } from 'react-router';
import { syncHistoryWithStore } from 'react-router-redux';
import store from '../store';

import App from './app';
import { default as AuthLayout } from '../modules/auth/components/auth-layout';
import { default as NotFound } from '../modules/not-found/components/not-found-layout';

// Create an enhanced history that syncs navigation events with the store
const history = syncHistoryWithStore(hashHistory, store);

const Root = () => (
    <Provider store={ store }>
        <Router history={ history }>
            <Route path="/" component={ App }>
                <IndexRoute component={ AuthLayout } />
                <Route path="*" component={ NotFound } />
            </Route>
            
        </Router>
    </Provider>
);

export default Root;