import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { RouteNames } from "../../constants";
import AnaliticariService from "../../services/AnaliticariService";
import { useEffect } from "react";
import { useState } from "react";


export default function AnaliticariPromjeni() {

    const navigate = useNavigate()
    const params = useParams()
    const [analiticar, setAnaliticari] = useState({})

    useEffect(() => {
        async function ucitajAnaliticara() {
            const odgovor = await AnaliticariService.getBySifra(params.sifra);
            setAnaliticari(odgovor);
        }

        ucitajAnaliticara();
    }, [params.sifra]);

    async function promjeni(sifra, analiticar) {

        const odgovor = await AnaliticariService.promjeni(sifra, analiticar);
        navigate(RouteNames.ANALITICARI_PREGLED);
        
    }


    function odradiSubmit (e) {
        e.preventDefault();

        let podaci = new FormData(e.target);

        promjeni(
            params.sifra,
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

        Promjeni analitičara
        <Form onSubmit={odradiSubmit}>

            <Form.Group controlId="ime">
                <Form.Label>Ime analitičara</Form.Label>
                <Form.Control type="text" name="ime" required
                defaultValue={analiticar.ime} />
            </Form.Group>

            <Form.Group controlId="prezime">
                <Form.Label>Prezime analitičara</Form.Label>
                <Form.Control type="text" name="prezime" required
                defaultValue={analiticar.prezime} />
            </Form.Group>

            <Form.Group controlId="kontakt">
                <Form.Label>Kontakt analitičara</Form.Label>
                <Form.Control type="text" name="kontakt" required
                defaultValue={analiticar.kontakt} />
            </Form.Group>

            <Form.Group controlId="strucnaSprema">
                <Form.Label>Stručna sprema analitičara</Form.Label>
                <Form.Control type="text" name="strucnaSprema" required
                defaultValue={analiticar.strucnaSprema} />
            </Form.Group>

            <hr style={{marginTop: '50px'}} />

            <Row>
                <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
                    <Link to={RouteNames.ANALITICARI_PREGLED}
                    className="btn btn-danger">Odustani</Link>
                </Col>
                <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
                    <Button variant="success" type="submit">
                        Promjeni analitičara
                    </Button>
                </Col>


            </Row>

        </Form>

        
        



        </>
    )
}