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
import AnaliticariPregled from './pages/analiticari/AnaliticariPregled';
import AnaliticariDodaj from './pages/analiticari/AnaliticariDodaj';
import AnaliticariPromjena from './pages/analiticari/AnaliticariPromjena';
import UzorcitlaPregled from './pages/uzorcitla/UzorcitlaPregled';
import UzorcitlaDodaj from './pages/uzorcitla/UzorcitlaDodaj';
import UzorcitlaPromjena from './pages/uzorcitla/UzorcitlaPromjena';
import AnalizePregled from './pages/analize/AnalizePregled';
import AnalizeDodaj from './pages/analize/AnalizeDodaj';
import AnalizePromjena from './pages/analize/AnalizePromjena';



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
        <Route path= {RouteNames.ANALITICARI_PREGLED} element={<AnaliticariPregled/>} />
        <Route path = {RouteNames.ANALITICARI_NOVI} element={<AnaliticariDodaj/>} />
        <Route path={RouteNames.ANALITICARI_PROMJENA} element={<AnaliticariPromjena />} />
        <Route path= {RouteNames.UZORCITLA_PREGLED} element={<UzorcitlaPregled/>} />
        <Route path = {RouteNames.UZORCITLA_NOVI} element={<UzorcitlaDodaj/>} />
        <Route path={RouteNames.UZORCITLA_PROMJENA} element={<UzorcitlaPromjena />} />
        <Route path= {RouteNames.ANALIZA_PREGLED} element={<AnalizePregled/>} />
        <Route path = {RouteNames.ANALIZA_NOVI} element={<AnalizeDodaj/>} />
        <Route path={RouteNames.ANALIZA_PROMJENA} element={<AnalizePromjena />} />
        


        
      </Routes>
      </Container>
      
      <hr  />
      &copy; Katarina Živković
    </Container>
  )
}

export default App
