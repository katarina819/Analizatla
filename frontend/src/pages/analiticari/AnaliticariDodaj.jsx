import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import AnaliticariService from "../../services/AnaliticariService";


export default function AnaliticariDodaj() {

    const navigate = useNavigate();

    async function dodaj(analiticar) {

        const odgovor = await AnaliticariService.dodaj(analiticar);
        navigate(RouteNames.ANALITICARI_PREGLED);
        
    }


    function odradiSubmit (e) {
        e.preventDefault();

        let podaci = new FormData(e.target);

        dodaj(
            {
                ime: podaci.get('ime'),
                prezime: podaci.get('prezime'),
                kontakt: podaci.get('kontakt'),
                strucnaSprema: podaci.get('strucnaSprema'),
            }


        )
    }



    return (
        <>

        Dodavanje analitičara
        <Form onSubmit={odradiSubmit}>

            <Form.Group controlId="ime">
                <Form.Label>Ime analitičara</Form.Label>
                <Form.Control type="text" name="ime" required />
            </Form.Group>

                        <Form.Group controlId="prezime">
                <Form.Label>Prezime analitičara</Form.Label>
                <Form.Control type="text" name="prezime" required />
            </Form.Group>

                        <Form.Group controlId="kontakt">
                <Form.Label>Kontakt analitičara</Form.Label>
                <Form.Control type="text" name="kontakt" required />
            </Form.Group>

                        <Form.Group controlId="strucnaSprema">
                <Form.Label>Stručna sprema analitičara</Form.Label>
                <Form.Control type="text" name="strucnaSprema" required />
            </Form.Group>

            <hr style={{marginTop: '50px'}} />

            <Row>
                <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                    <Link to={RouteNames.ANALITICARI_PREGLED}
                    className="btn btn-danger">Odustani</Link>
                </Col>
                <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                    <Button variant="success" type="submit">
                        Dodaj analitičara
                    </Button>
                </Col>


            </Row>

        </Form>

        </>
    )
}