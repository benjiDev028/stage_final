import React from "react";
import './Header.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import { Link } from "react-router-dom";


const Header = () => {
  return (
    <nav className="navbar navbar-expand-lg ">
      <div className="container-fluid">
        <a className="navbar-brand" href="/">Sentiment Analysis</a>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto">
            <li className="nav-item">
              <a className="nav-link" href="/#">Accueil</a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/#">Learning</a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/#">Contact</a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/#">Services</a>
            </li>
            <li className="nav-item">
              <Link to='/login' className="nav-link" >Sign In</Link>
            </li>
            </ul>
        </div>
      </div>
    </nav>
    

  );
}

export default Header;
