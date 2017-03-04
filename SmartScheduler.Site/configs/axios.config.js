import store from '../src/store';
import { push } from 'react-router-redux';
import axios from 'axios';

/**
 * Axios request interceptor
 * Adds 'X-Auth-Token' to every axios request (except authorisation request)
 */
axios.interceptors.request.use(config => {

    config.headers = {
        'X-Auth-Token': store.getState().auth.profile.token
    }

    return config;
});

/**
 * Axios response interceptor
 * Handle every response and do appropriate redirection
 */
axios.interceptors.response.use(
    response => response, 
    error => {
        const status = error.response.status;
        handleStatus(status);
        return Promise.reject(error);
    }
);

const handleStatus = status => {
    switch(status) {
        case 401:
            store.dispatch('/login');
            break;
        default:
            store.dispatch(push('/not-found'));
            break;
    }
}