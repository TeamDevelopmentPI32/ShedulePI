import React, { Component } from 'react';
import { Link } from 'react-router';

const NotFoundComponent = () => (
    <div className="l-not-found">
        <header className="b-not-found__header">
            <h1 className="b-not-found__title">
                Ops! Something went wrong...
            </h1>
        </header>
        <Link to="/" className="b-not-found__link">
            Go to the home page
        </Link>
    </div>
);

export default NotFoundComponent;