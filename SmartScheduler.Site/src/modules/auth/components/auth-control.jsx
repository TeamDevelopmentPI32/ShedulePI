import React, { Component } from 'react';

class AuthControlComponent extends Component {

    // Check, if component was touched and still has no values
    isInvalid = () => {
        const { meta: { touched, error } } = this.props;
        return touched && error;
    }

    // get className according too input state
    handleClassName = () => {
        const invalid = this.isInvalid();
        const className = 'b-form__form-control';

        return invalid 
            ? `${className} ${className}--invalid`
            : className;
    }

    render() {
        const { id, name, type, label, input, meta: { error } } = this.props;
        return (
            <section className="b-form__form-group">
                <label 
                    className="b-form__label" 
                    htmlFor={ id }>
                    { label }
                </label>
                <input
                    { ...input }
                    className={ this.handleClassName() } 
                    type={ type }
                    name={ name }
                    id={ id } />
                { this.isInvalid() && <span className="b-form__error-message">{ error }</span> }
            </section>
        );
    }
}

export default AuthControlComponent;