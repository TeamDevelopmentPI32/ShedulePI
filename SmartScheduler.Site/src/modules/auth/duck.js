import Crud from '../../utils/api/crud';
import API from '../../utils/api/paths';

// Actions
const SIGNIN_REQUEST = 'ss/auth/SIGNIN_REQUEST';
const SIGNIN_FULFILLED = 'ss/auth/SIGNIN_FULFILLED';
const SIGNIN_FAILURE = 'ss/auth/SIGNIN_FAILURE';
const SIGNOUT = 'ss/auth/SIGNOUT';

// Init state
const initState = {
    token: null,
}

// Reducer
const reducer = (state = initState, action) => {
    switch(action.type) {
        case SIGNIN_REQUEST:
            return state;
        case SIGNIN_FULFILLED:
        case SIGNIN_FAILURE: 
            return {
                ...state,
                token: action.token
            }
        default:
            return state;
    }
}

// Action creators
const signInRequest = () => ({
    type: SIGNIN_REQUEST
});

const signInFulfilled = token => ({
    type: SIGNIN_FULFILLED,
    token
});

const signInFailure = () => ({
    type: SIGNIN_FAILURE,
    token: null
});


// Async actions
const signIn = credentials => dispatch => {
    dispatch(SIGNINRequest());

    return Crud.post(API.auth.signIn)
        .then(response => {
            const token = response.data.token;
            dispatch(SIGNINFulfilled(token));
        })
        .catch(error => {
            dispatch(SIGNINFailure());
        });
}

const signOut = () => dispatch => {
    return Crud.post(API.auth.signOut);
}

// Exports
export const actionTypes = {
    SIGNIN_REQUEST,
    SIGNIN_FULFILLED,
    SIGNIN_FAILURE,
    SIGNOUT
}

export const actionCreators = {
    signIn,
    signOut
}

export default reducer;