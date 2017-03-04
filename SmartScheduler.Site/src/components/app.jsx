import React from 'react';

import { default as Header } from './header';
import { default as Main } from './main';
import { default as Footer } from './footer';

const App = ({ children }) => (
    <div className="l-app">
        <Header />
        <Main>
            { children }
        </Main>
        <Footer />
    </div>
);

export default App;