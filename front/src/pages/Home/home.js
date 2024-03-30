// Home.js
import React from 'react';
import '../Home/home.css';

import Cards from '../../components/Cards/Cards';
import Header from '../../components/Header/Header';
import Main from '../../components/Main/Main';
import App from '../../components/Chatbot/chatbot';
import Commentaires from '../../components/Commentaires/commentaires';
import Footer from '../../components/Footer/footer';


function Home() {
  return (
    <div className="home">
     <Header/>
     <Main/>
     <Cards/>
     <App/>
     <Commentaires/>
     <br/>
     <Footer/>
    </div>
  );
}

export default Home;
