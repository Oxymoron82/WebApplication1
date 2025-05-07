import React, { useState } from 'react';
import { GoogleLogin } from '@react-oauth/google';
import './App.css';
import logo from './logo.svg';

function App() {
  const [user, setUser] = useState(null);

  // Функция, которая будет вызываться при успешном входе
  const handleLoginSuccess = (response) => {
    console.log('Login Success:', response);
    const token = response.credential; // Это Google OAuth токен

    // Сохраняем токен в localStorage (или куки, в зависимости от требований)
    localStorage.setItem('token', token);
    setUser(token);  // Сохраняем токен в состоянии, чтобы отображать информацию о пользователе

    // Отправка токена на сервер для проверки
    fetch('/api/your-protected-endpoint', {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${token}`, // Отправка токена в заголовке
      },
    })
      .then((res) => res.json())
      .then((data) => console.log('Server response:', data))
      .catch((error) => console.error('Error:', error));
  };

  // Функция, которая будет вызываться при ошибке входа
  const handleLoginFailure = (error) => {
    console.log('Login Failed:', error);
  };

  // Функция для выхода
  const handleLogout = () => {
    localStorage.removeItem('token');
    setUser(null);
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        
        {/* Если пользователь не авторизован, показываем кнопку входа */}
        {!user ? (
          <GoogleLogin
            onSuccess={handleLoginSuccess}   // Успешный вход
            onError={handleLoginFailure}     // Ошибка при входе
          />
        ) : (
          <div>
            <h2>Welcome, User!</h2>
            <button onClick={handleLogout}>Logout</button>
          </div>
        )}

        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
