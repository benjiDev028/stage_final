import React, { useState } from 'react';
import './register.css'; // Assurez-vous de créer le fichier CSS correspondant pour le style de cette page
import Header from '../../components/Header/Header';
import { Link } from 'react-router-dom';
import {RegisterUser} from '../../services/AuthService';
import Main from '../../components/Main/Main';
import Footer from '../../components/Footer/footer';

function Register() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [username,setUserName] = useState('');
  const [errorMessage,setErrorMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    try {
     const  response = await RegisterUser( firstName, lastName,email,username, password);
      if (!response.ok) Error(response.Error); 
      
      alert("compte Done ") ;window.location.replace('/login');
    } catch (error) {
      console.error('Erreur lors de la création du compte:', error.message);
      setErrorMessage("email ou pseudo deja utilise "); 
     
    }
  };

  return (
    <div>
      <Header />
      <Main/><br/><br/><br/>
        <div className="conta">
        <div className="screenR">
          <div className="screen__content">
            <form className="login" onSubmit={handleSubmit}>
              <div className="login__field">
                <i className="login__icon fas fa-user"></i>
                <input
                  type="text"
                  id='firstName'
                  value={firstName}
                  className="login__input"
                  placeholder="First Name"
                  onChange={(e) => setFirstName(e.target.value)} required
                />
              </div>
              <div className="login__field">
                <i className="login__icon fas fa-user"></i>
                <input
                  type="text"
                  id='lastName'
                  value={lastName}
                  className="login__input"
                  placeholder="Last Name"
                  onChange={(e) => setLastName(e.target.value)} required
                />
              </div>
              <div className="login__field">
                <i className="login__icon fas fa-user"></i>
                <input
                  type="text"
                  id='username'
                  value={username}
                  className="login__input"
                  placeholder="username"
                  onChange={(e) => setUserName(e.target.value)} required
                />
              </div>
              <div className="login__field">
                <i className="login__icon fas fa-envelope"></i>
                <input
                  type="email"
                  id='email'
                  value={email}
                  className="login__input"
                  placeholder="Email"
                  onChange={(e) => setEmail(e.target.value)} required
                />
              </div>
              <div className="login__field">
                <i className="login__icon fas fa-lock"></i>
                <input
                  type="password"
                  id='password'
                  value={password}
                  className="login__input"
                  placeholder="Password"
                  onChange={(e) => setPassword(e.target.value)} required
                />
              </div>
              <button type="submit" className="button login__submit">
                <span className="button__text">Sign Up</span>
                <i className="button__icon fas fa-chevron-right"></i>
              </button>
            </form>
            <div className="social-login">
              <h3>Analysis S</h3>
              <div className="social-icons"></div>
            </div>
            <div className="log">
              <p className='error'>{errorMessage && <div className="error-message">{errorMessage}</div>} </p>
            </div>
            <div className="login__create-account">
            
              <p className='lien'> <Link to='/login' className="nav-link" > vous avez un compte ? Sign In</Link></p>
            
            </div>
          </div>
          <div className="screen__background">
            <span className="screen__background__shape screen__background__shape4"></span>
            <span className="screen__background__shape screen__background__shape3"></span>
            <span className="screen__background__shape screen__background__shape2"></span>
            <span className="screen__background__shape screen__background__shape1"></span>
          </div>
        </div>
      </div><br/><br/>
      
      <Footer/>
      
    </div>
    
  );
}

export default Register;
