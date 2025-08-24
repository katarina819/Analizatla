import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { RouteNames, PRODUKCIJA } from '../constants';
import useAuth from '../hooks/useAuth';

export default function NavBarEdunova() {
  const navigate = useNavigate();
  const { logout, isLoggedIn } = useAuth();

  function OpenSwaggerURL() {
    window.open(PRODUKCIJA + "/swagger/index.html", "_blank");
  }

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand className="ruka" onClick={() => navigate(RouteNames.HOME)}>
          Analiza tla
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">

            <Nav.Link onClick={() => navigate(RouteNames.HOME)}>Početna</Nav.Link>

            {isLoggedIn ? (
              <>
                <Nav.Link onClick={() => navigate(RouteNames.NADZORNA_PLOCA)}>
                  Nadzorna ploča
                </Nav.Link>

                <NavDropdown title="Programi" id="basic-nav-dropdown">
                  <NavDropdown.Item onClick={() => navigate(RouteNames.SMJER_PREGLED)}>
                    Lokacije
                  </NavDropdown.Item>
                  <NavDropdown.Item onClick={() => navigate(RouteNames.ANALITICARI_PREGLED)}>
                    Analitičari
                  </NavDropdown.Item>
                  <NavDropdown.Item onClick={() => navigate(RouteNames.UZORCITLA_PREGLED)}>
                    Uzorci Tla
                  </NavDropdown.Item>
                  <NavDropdown.Item onClick={() => navigate(RouteNames.ANALIZA_PREGLED)}>
                    Analiza Tla
                  </NavDropdown.Item>
                </NavDropdown>

                <Nav.Link onClick={() => OpenSwaggerURL()}>Swagger</Nav.Link>
                <Nav.Link onClick={() => navigate(RouteNames.ERA)}>ERA dijagram</Nav.Link>
                <Nav.Link onClick={logout}>Odjava</Nav.Link>
              </>
            ) : (
              <Nav.Link onClick={() => navigate(RouteNames.LOGIN)}>Prijava</Nav.Link>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}
