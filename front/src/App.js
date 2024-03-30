import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from './pages/Home/home.js';
import Login from './pages/Login/login';
import Register from './pages/Register/register.jsx';
import Method from './pages/methods/method.jsx';
import ChatBot from './components/Chatbot/chatbot.jsx'; // Assurez-vous que le chemin est correct


import ModelP from './pages/page_model/Model.jsx';
import GoogleP from './pages/page_google/page_google.jsx';


function App() {
  return (
    <Router>
      <div className="App">
   

        
        <Routes>
        
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/method" element={<Method />} />
          {/* Si ChatBot doit être accessible via une route spécifique */}
          <Route path="/chatbox" element={<ChatBot />} />
          <Route path='/method/google' element ={<GoogleP/>}/>
          <Route path ='method/internal' element ={<ModelP/>}/>          
        </Routes>
       
      </div>
    </Router>
  );
}

export default App;
