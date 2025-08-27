import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import UzorcitlaService from "../../services/UzorcitlaService";
import { useEffect } from "react";
import { useState } from "react";


export default function UzorcitlaPromjeni() {

    const navigate = useNavigate()
    const params = useParams()
    const [uzorcitla, setUzorciTla] = useState({})

    useEffect(() => {
        async function ucitajUzorciTla() {
            const odgovor = await UzorcitlaService.getBySifra(params.sifra);
            setUzorciTla(odgovor);
        }

        ucitajUzorciTla();
    }, [params.sifra]);

    async function promjeni(sifra, uzorcitla) {

        const odgovor = await UzorcitlaService.promjeni(sifra, uzorcitla);
        navigate(RouteNames.UZORCITLA_PREGLED);
        
    }


    function odradiSubmit (e) {
        e.preventDefault();

        let podaci = new FormData(e.target);

        promjeni(
            params.sifra,
            {
                masaUzorka: parseFloat(podaci.get('masaUzorka')),
                vrstaTla: podaci.get('vrstaTla'),
                datum: podaci.get('datum'),
                mjestoUzorkovanja: podaci.get('lokacija')
            }
        
        )
    }



    return (
        <>

        Promjeni uzorak tla
        <Form onSubmit={odradiSubmit}>

            <Form.Group controlId="masaUzorka">
                <Form.Label>Masa uzorka</Form.Label>
                <Form.Control type="number" name="masaUzorka" required
                step="0.01"
                defaultValue={uzorcitla.masaUzorka} />
            </Form.Group>

            <Form.Group controlId="vrstaTla">
                <Form.Label>Vrsta tla</Form.Label>
                <Form.Control type="text" name="vrstaTla" required
                defaultValue={uzorcitla.vrstaTla} />
            </Form.Group>

            <Form.Group controlId="datum">
                <Form.Label>Datum</Form.Label>
                <Form.Control type="date" name="datum" required
                defaultValue={uzorcitla.datum?.split('T')[0]} />
            </Form.Group>

            <Form.Group controlId="lokacija">
                <Form.Label>Lokacija</Form.Label>
                <Form.Control
                    type="text"
                    name="lokacija"
                    required
                    defaultValue={uzorcitla.mjestoUzorkovanja || ''}
                />
            </Form.Group>


            <hr style={{marginTop: '50px'}} />

            <Row>
                <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                    <Link to={RouteNames.UZORCITLA_PREGLED}
                    className="btn btn-danger">Odustani</Link>
                </Col>
                <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                    <Button variant="success" type="submit">
                        Promjeni uzorak tla
                    </Button>
                </Col>


            </Row>

        </Form>

        </>
    )
}