import React from 'react';
import { Provider } from 'react-redux';
import { Router, hashHistory, Route, IndexRoute } from 'react-router';
import store from '../store';

import App from './app';
import { default as AuthLayout } from '../modules/auth/components/auth-layout';

const Root = () => (
    <Provider store={ store }>
        <Router history={ hashHistory }>
            <Route path="/" component={ App }>
                <IndexRoute component={ AuthLayout } />
            </Route>
        </Router>
    </Provider>
);

export default Root;