import React from 'react';
import { Field, reduxForm } from 'redux-form';
import { connect } from 'react-redux';

import { default as Button } from '../../../components/button';
import { default as AuthControl } from './auth-control';
import { actionCreators } from '../';

const AuthFormComponent = ({ 
    handleClose,
    pristine,
    valid,
    meta,
    handleSubmit,
    signIn
}) => (
    <form className="b-form" onSubmit={ handleSubmit(signIn) } >

        <Field 
            component={ AuthControl } 
            type="text"
            id="login"
            name="username"
            label="Username" />

        <Field 
            component={ AuthControl } 
            type="password"
            id="password"
            name="password"
            label="Password" />

        <section className="b-form__actions">
            <Button type="submit" disabled={ !valid } title="Sign In" className="btn btn--green btn--fill" />
            <Button type="button" title="Close" className="btn btn--orange" onClick={ handleClose }/>
        </section>
        
    </form>
);

// Validation of redux-form fields
const validate = values => {
    const errors = {};

    if(!values.username) {
        errors.username = 'Username is required'
    }

    if(!values.password) {
        errors.password = 'Password is required'
    }

    return errors;
}

// Connect form to 'redux-form' and pass appropriate validation function to it
const form = reduxForm({ 
    form: 'AuthForm',
    validate 
})(AuthFormComponent);

const mapDispatchToProps = dispatch => ({
    signIn(credentials) {
        return dispatch(actionCreators.signIn(credentials));
    }
});

export default connect(null, mapDispatchToProps)(form)