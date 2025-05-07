import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { GoogleOAuthProvider } from '@react-oauth/google';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <GoogleOAuthProvider clientId="533848342255-88co371a1clr84im2i5tnjdkj8m9hucu.apps.googleusercontent.com"
    >




      <App />
    </GoogleOAuthProvider>
  </React.StrictMode>
);


reportWebVitals();
