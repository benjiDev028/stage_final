// Home.js
import React from 'react';

import Cards from '../../components/Cards/Cards';
import Header from '../../components/Header/Header';
import Main from '../../components/Main/Main';


function Home() {
  return (
    <div className="App">
     <Header/>
     <Main/>
     <Cards/>
    </div>
  );
}

export default Home;
