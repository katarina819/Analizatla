import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import LokacijeService from "../../services/LokacijeService";
import { useEffect } from "react";
import { useState } from "react";


export default function LokacijePromjena() {

    const navigate = useNavigate()
    const params = useParams()
    const [lokacija, setLokacije] = useState({})

    useEffect(() => {
        async function ucitajLokaciju() {
            const odgovor = await LokacijeService.getBySifra(params.sifra);
            setLokacije(odgovor);
        }

        ucitajLokaciju();
    }, [params.sifra]);

    async function promjena(sifra, lokacija) {

        const odgovor = await LokacijeService.promjeni(sifra, lokacija);
        navigate(RouteNames.SMJER_PREGLED);
        
    }


    function odradiSubmit (e) {
        e.preventDefault();

        let podaci = new FormData(e.target);

        promjena(
            params.sifra,
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
                <Form.Control type="text" name="mjestoUzorkovanja" required
                defaultValue={lokacija.mjestoUzorkovanja} />
            </Form.Group>

            <hr style={{marginTop: '50px'}} />

            <Row>
                <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                    <Link to={RouteNames.SMJER_PREGLED}
                    className="btn btn-danger">Odustani</Link>
                </Col>
                <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                    <Button variant="success" type="submit">
                        Promjeni lokaciju
                    </Button>
                </Col>


            </Row>

        </Form>

        
        



        </>
    )
}