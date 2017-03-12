import { combineReducers } from 'redux';

// Imported reducers
import { reducer as authReducer } from './modules/auth';

const rootReducer = combineReducers({
    authReducer
});

export default rootReducer;