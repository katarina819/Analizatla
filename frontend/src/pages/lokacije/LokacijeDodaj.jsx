import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import LokacijeService from "../../services/LokacijeService";


export default function LokacijeDodaj() {

    const navigate = useNavigate();

    async function dodaj(lokacija) {

        const odgovor = await LokacijeService.dodaj(lokacija);
        navigate(RouteNames.SMJER_PREGLED);
        
    }


    function odradiSubmit (e) {
        e.preventDefault();

        let podaci = new FormData(e.target);

        dodaj(
            {
                mjestoUzorkovanja: podaci.get('mjestoUzorkovanja')
            }


        )
    }



    return (
        <>

        Dodavanje lokacije
        <Form onSubmit={odradiSubmit}>

            <Form.Group controlId="mjestoUzorkovanja">
                <Form.Label>Naziv lokacije</Form.Label>
                <Form.Control type="text" name="mjestoUzorkovanja" required />
            </Form.Group>

            <hr style={{marginTop: '50px'}} />

            <Row>
                <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                    <Link to={RouteNames.SMJER_PREGLED}
                    className="btn btn-danger">Odustani</Link>
                </Col>
                <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                    <Button variant="success" type="submit">
                        Dodaj lokaciju
                    </Button>
                </Col>


            </Row>

        </Form>

        
        



        </>
    )
}