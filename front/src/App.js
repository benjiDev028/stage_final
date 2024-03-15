
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import  Header from './components/Header/Header';
import Main from './components/Main/Main';
import Cards from './components/Cards/Cards.jsx';
function App() {
  return (
    
    <div className="App">
     <Header/>
     <Main/>
     <Cards/>
    </div>
  );
}

export default App;
