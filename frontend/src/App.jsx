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
import LoadingSpinner from './components/LoadingSpinner'
import Login from "./pages/Login"
import useAuth from "./hooks/useAuth"
import NadzornaPloca from './pages/NadzornaPloca'
import useError from "./hooks/useError"
import ErrorModal from "./components/ErrorModal"
import EraDijagram from './pages/EraDiagram'
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  const { isLoggedIn } = useAuth();
  const { errors, prikaziErrorModal, sakrijError } = useError();

  function godina() {
    const pocenta = 2025;
    const trenutna = new Date().getFullYear();
    if (pocenta === trenutna) return trenutna;
    return pocenta + ' - ' + trenutna;
  }

  return (
    <>
      <LoadingSpinner />
      <ErrorModal show={prikaziErrorModal} errors={errors} onHide={sakrijError} />
      <Container>
        <NavBarEdunova />
        <Container className="app">
          <Routes>
            <Route path={RouteNames.HOME} element={<Pocetna />} />
            {isLoggedIn ? (
              <>
                <Route path={RouteNames.NADZORNA_PLOCA} element={<NadzornaPloca />} /> 
                <Route path={RouteNames.SMJER_PREGLED} element={<LokacijePregled />} />
                <Route path={RouteNames.SMJER_NOVI} element={<LokacijeDodaj />} />
                <Route path={RouteNames.SMJER_PROMJENA} element={<LokacijePromjena />} />

                <Route path={RouteNames.ANALITICARI_PREGLED} element={<AnaliticariPregled />} />
                <Route path={RouteNames.ANALITICARI_NOVI} element={<AnaliticariDodaj />} />
                <Route path={RouteNames.ANALITICARI_PROMJENA} element={<AnaliticariPromjena />} />

                <Route path={RouteNames.UZORCITLA_PREGLED} element={<UzorcitlaPregled />} />
                <Route path={RouteNames.UZORCITLA_NOVI} element={<UzorcitlaDodaj />} />
                <Route path={RouteNames.UZORCITLA_PROMJENA} element={<UzorcitlaPromjena />} />

                <Route path={RouteNames.ANALIZA_PREGLED} element={<AnalizePregled />} />
                <Route path={RouteNames.ANALIZA_NOVI} element={<AnalizeDodaj />} />
                <Route path={RouteNames.ANALIZA_PROMJENA} element={<AnalizePromjena />} />

                <Route path={RouteNames.ERA} element={<EraDijagram />} />
              </>
            ) : (
              <Route path={RouteNames.LOGIN} element={<Login />} />
            )}
          </Routes>
          <ToastContainer position="top-center" autoClose={3000} />
        </Container>
        <hr />
          Katarina Živković &copy; {godina()}
      </Container>
    </>
  )
}

export default App;
