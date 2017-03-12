import React from 'react';

import { default as Button } from '../../../components/button';
import LogoIcon from '../../../assets/logo.png';

const AuthLayoutComponent = (props) => (
    <div className="l-auth">
        <div className="b-preview">
            <h1 className="b-preview__title">
                <img className="b-preview__logo" src={ LogoIcon } alt="Smart Scheduler"/>
                Smart scheduler
            </h1>
            <h4 className="b-preview__description">...is a smart way to orginize your day</h4>
        </div>
        <div className="b-auth">
            <Button type="button" className="btn btn--default" title="Continue as user" />
            <Button type="button" className="btn btn--default" title="Continue as guest" />
        </div>
    </div>
);

export default AuthLayoutComponent;