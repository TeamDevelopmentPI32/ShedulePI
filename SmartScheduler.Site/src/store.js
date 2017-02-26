import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';

// Combined reducer fot the store
import rootReducer from './reducer';

// Create app store
const store = createStore(
    rootReducer,
    applyMiddleware(thunk)
);

// Export store
export default store;