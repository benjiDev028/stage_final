
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'

import {BrowserRouter as  Router, Route, Routes} from "react-router-dom";
import Home from './pages/Home/home.js';
import Login from './pages/Login/login';

function App() {
  return (

    <Router>

    
    <div className="App">
      <Routes>
      <Route path="/" exact element={<Home/>} />
      <Route path="/login" element={<Login/>} />

      </Routes>
     
    </div>
    </Router>
  );
}

export default App;
