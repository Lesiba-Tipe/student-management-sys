import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import './index.css';
import Login from './components/account/login/login';

function App() {
  const handleLogin = (username: string, password: string) => {
    // Handle login logic here
    console.log('Username:', username);
    console.log('Password:', password);
  };

  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path='login' element={<Login onLogin={handleLogin} />} ></Route>
        </Routes>
      </Router>
    </div>
  );
}

export default App;
