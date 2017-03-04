import React from 'react';
import { render } from 'react-dom';

// Error interceptor & config creator
import '../configs/axios.config';

// Imports styles for the project
import './sass';

// Import root component for routing and stuff like this
import Root from './components/root';

// Bootstrap App
render(<Root />, document.querySelector("#root"));