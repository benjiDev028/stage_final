import React, { useState } from 'react';
import './login.css';
import Header from '../../components/Header/Header';
import { Link } from 'react-router-dom';
import { loginUser,getUserIdFromToken } from '../../services/AuthService'
import Main from '../../components/Main/Main';




// Utilisez la fonction isLoggedIn pour vérifier si l'utilisateur est connecté

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState(''); // Ajouté pour afficher les messages d'erreur

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage(''); // Réinitialise le message d'erreur précédent
    try {
      const token = await loginUser(email, password);
      if (token) {
        const userId = getUserIdFromToken(token);
        alert("ID utilisateur:" + userId);
        window.location.replace('/method');
        // Continuez avec l'ID utilisateur selon vos besoins
      }
    } catch (error) {
      console.error('Erreur de connexion:', error.message);
      setErrorMessage("email ou ot de passe incorrect"); // Met à jour le message d'erreur pour l'afficher
    }
  

};
  return (
    <div>
      <Header />
      <Main/><br/><br/><br/><br/><br/>

      <div className="container">
        <div className="screen">
          <div className="screen__content">
            <form className="login" onSubmit={handleSubmit}>
            
              <div className="login__field">
                <i className="login__icon fas fa-user"></i>
                <input
                  type="mail"
                  id='mail'
                  value={email}
                  className="login__input"
                  placeholder="User name / Email"
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
                <span className="button__text">Log In Now</span>
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
              <p className='link'>Vous n'avez pas de compte ? <Link className='link' to="/register">Créez-en un</Link></p>
            </div>
          </div>
          <div className="screen__background">
            <span className="screen__background__shape screen__background__shape4"></span>
            <span className="screen__background__shape screen__background__shape3"></span>
            <span className="screen__background__shape screen__background__shape2"></span>
            <span className="screen__background__shape screen__background__shape1"></span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;