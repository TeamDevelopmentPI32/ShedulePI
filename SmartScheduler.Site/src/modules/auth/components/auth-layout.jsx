import React, { Component } from 'react';

import { default as Button } from '../../../components/button';
import LogoIcon from '../../../assets/logo.png';
import { default as AuthModal } from './auth-modal';

class AuthLayoutComponent extends Component {

    state = {
        isAuthModalOpen: false,
    };

    toggleModal = () => {
        this.setState({ isAuthModalOpen: !this.state.isAuthModalOpen });
    };

    render() {
        const { isAuthModalOpen } = this.state;
        return (
            <div className="l-auth">
                <div className="b-preview">
                    <h1 className="b-preview__title">
                        <img className="b-preview__logo" src={ LogoIcon } alt="Smart Scheduler"/>
                        Smart scheduler
                    </h1>
                    <h4 className="b-preview__description">...is a smart way to orginize your day</h4>
                </div>
                <div className="b-auth">
                    <Button 
                        type="button" 
                        className="btn btn--default" 
                        title="Continue as user" 
                        onClick={ this.toggleModal } />

                    <AuthModal 
                        isOpen={ isAuthModalOpen } 
                        onRequestClose={ this.toggleModal }
                        name="Authorization"
                        title="Sign in" />

                    <Button
                        type="button"
                        className="btn btn--default"
                        title="Continue as guest"
                        onClick={ this.handleA } />
                </div>
            </div>
        );
    }
}

export default AuthLayoutComponent;