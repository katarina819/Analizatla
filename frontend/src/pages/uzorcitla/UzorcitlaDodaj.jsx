import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import UzorcitlaService from "../../services/UzorcitlaService";


export default function UzorcitlaDodaj() {

    const navigate = useNavigate();



    async function dodaj(uzorcitla) {

        const odgovor = await UzorcitlaService.dodaj(uzorcitla);
        navigate(RouteNames.UZORCITLA_PREGLED);
        
    }


    function odradiSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);

    dodaj({
        masaUzorka: parseFloat(podaci.get('masaUzorka')),
        vrstaTla: podaci.get('vrstaTla'),
        datum: podaci.get('datum'),
        mjestoUzorkovanja: podaci.get('lokacija')
            }


        )
    }



    return (
        <>

        Dodavanje novog uzorka tla
        <Form onSubmit={odradiSubmit}>

            <Form.Group controlId="masaUzorka">
                <Form.Label>Masa uzorka (g)</Form.Label>
                <Form.Control
                    type="number"
                    name="masaUzorka"
                    required
                    step="0.01"   // dozvoljava decimalne vrijednosti
                    min="0"       // opcionalno, sprječava negativne brojeve
                />
            </Form.Group>

                        <Form.Group controlId="vrstaTla">
                <Form.Label>Vrsta tla</Form.Label>
                <Form.Control type="text" name="vrstaTla" required />
            </Form.Group>

                        <Form.Group controlId="datum">
                <Form.Label>Datum</Form.Label>
                <Form.Control type="date" name="datum" required />
            </Form.Group>

                        <Form.Group controlId="lokacija">
                <Form.Label>Lokacija</Form.Label>
                <Form.Control type="text" name="lokacija" required />
            </Form.Group>

            <hr style={{marginTop: '50px'}} />

            <Row>
                <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                    <Link to={RouteNames.UZORCITLA_PREGLED}
                    className="btn btn-danger">Odustani</Link>
                </Col>
                <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                    <Button variant="success" type="submit">
                        Dodaj uzorak tla
                    </Button>
                </Col>


            </Row>

        </Form>

        
        



        </>
    )
}