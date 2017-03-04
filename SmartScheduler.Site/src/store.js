import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { hashHistory } from 'react-router';
import { routerMiddleware } from 'react-router-redux';

// Combined reducer fot the store
import rootReducer from './reducer';

const router = routerMiddleware(hashHistory);
const midlewares = [ router, thunk ];

// Create app store
const store = createStore(
    rootReducer,
    applyMiddleware(...midlewares)
);

// Export store
export default store;