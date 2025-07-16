import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css'
import { Container } from 'react-bootstrap'
import NavBarEdunova from './components/NavBarEdunova'
import { Routes, Route } from 'react-router-dom';
import { RouteNames } from './constants'
import Pocetna from './pages/Pocetna'
import LokacijePregled from './pages/lokacije/LokacijePregled';


function App() {
  

  return (
    <Container>
      <NavBarEdunova/>

      <Container className="app">
      <Routes>
        <Route path={RouteNames.HOME} element={<Pocetna />} />
        <Route path= {RouteNames.SMJER_PREGLED} element={<LokacijePregled/>} />
        
      </Routes>
      </Container>
      
      <hr  />
      &copy; Edunova
    </Container>
  )
}

export default App
