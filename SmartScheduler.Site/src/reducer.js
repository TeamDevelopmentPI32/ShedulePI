import { combineReducers } from 'redux';

// Imported reducers
import { reducer as authReducer } from './modules/auth';
import { reducer as formReducer } from 'redux-form'
import { routerReducer } from 'react-router-redux'

const rootReducer = combineReducers({
    auth: authReducer,
    form: formReducer,
    routing: routerReducer
});

export default rootReducer;