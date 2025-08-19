import { useEffect, useState } from "react";
import { Button, Container, Row, Col, Card } from "react-bootstrap";
import AnalizeService from "../../services/AnalizeService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";

export default function AnalizePregled() {
  const [analize, setAnalize] = useState([]);
  const navigate = useNavigate();

  async function dohvatiAnalize() {
    try {
      const odgovor = await AnalizeService.get();
      setAnalize(odgovor);
    } catch (error) {
      console.error("Greška pri dohvaćanju analiza:", error);
    }
  }

  useEffect(() => {
    dohvatiAnalize();
  }, []);

  function formatirajDatum(datumISO) {
    if (!datumISO) return "";
    const d = new Date(datumISO);
    const dan = String(d.getDate()).padStart(2, "0");
    const mjesec = String(d.getMonth() + 1).padStart(2, "0");
    const godina = d.getFullYear();
    return `${dan}-${mjesec}-${godina}`;
  }

  async function obrisi(sifra) {
    if (!window.confirm("Sigurno obrisati ovu analizu?")) return;
    try {
      await AnalizeService.obrisi(sifra);
      dohvatiAnalize();
    } catch (error) {
      console.error("Greška pri brisanju:", error);
    }
  }

  return (
    <Container className="mt-4">
      <Link className="btn btn-success mb-3" to={RouteNames.ANALIZA_NOVI}>
        Dodavanje analize
      </Link>

      <Row xs={1} md={2} lg={3} className="g-4">
        {analize.map((u) => (
          <Col key={u.sifra}>
            <Card>
              <Card.Body>
                <Card.Title>{u.vrstaTla} - {u.mjestoUzorkovanja}</Card.Title>
                <Card.Text>
                  <strong>pH:</strong> {u.pHVrijednost}<br/>
                  <strong>Fosfor:</strong> {u.fosfor}<br/>
                  <strong>Kalij:</strong> {u.kalij}<br/>
                  <strong>Magnezij:</strong> {u.magnezij}<br/>
                  <strong>Karbonati:</strong> {u.karbonati}<br/>
                  <strong>Humus:</strong> {u.humus}<br/>
                  <strong>Masa uzorka:</strong> {u.masaUzorka}<br/>
                  <strong>Analitičar:</strong> {u.ime} {u.prezime}<br/>
                  <strong>Kontakt:</strong> {u.kontakt}<br/>
                  <strong>Stručna sprema:</strong> {u.strucnaSprema}<br/>
                  <strong>Datum uzorkovanja:</strong> {formatirajDatum(u.datumUzorka)}<br/>
                  <strong>Datum analize:</strong> {formatirajDatum(u.datum)}
                </Card.Text>
                <Button variant="primary" onClick={() => navigate(`/analiza/${u.sifra}`)}>
                  Promjena
                </Button>{" "}
                <Button variant="danger" onClick={() => obrisi(u.sifra)}>
                  Obriši
                </Button>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}
