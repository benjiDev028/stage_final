import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import './Main.css';
import mainpic from '../../assets/mainpic.jpg';

const Main = () => {
  return (
    <>
      <div style={{
          background: `url(${mainpic}) no-repeat center center`, 
          backgroundSize: 'cover',
          minHeight: '50vh', // Ici, nous définissons une hauteur minimale de 50% de la hauteur de la fenêtre
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center', // Cela alignera le bouton verticalement au centre
        }} className="jumbotron bg-cover text-white">
         
      </div>

      
    </>
  );
};

export default Main;
