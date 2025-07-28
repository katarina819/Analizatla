import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css'
import { Container } from 'react-bootstrap'
import NavBarEdunova from './components/NavBarEdunova'
import { Routes, Route } from 'react-router-dom';
import { RouteNames } from './constants'
import Pocetna from './pages/Pocetna'
import LokacijePregled from './pages/lokacije/LokacijePregled';
import LokacijeDodaj from './pages/lokacije/LokacijeDodaj';
import LokacijePromjena from './pages/lokacije/LokacijePromjena';


function App() {
  

  return (
    <Container>
      <NavBarEdunova/>

      <Container className="app">
      <Routes>
        <Route path={RouteNames.HOME} element={<Pocetna />} />
        <Route path= {RouteNames.SMJER_PREGLED} element={<LokacijePregled/>} />
        <Route path = {RouteNames.SMJER_NOVI} element={<LokacijeDodaj/>} />
        <Route path = {RouteNames.SMJER_PROMJENA} element={<LokacijePromjena/>} />
        
      </Routes>
      </Container>
      
      <hr  />
      &copy; Edunova
    </Container>
  )
}

export default App
