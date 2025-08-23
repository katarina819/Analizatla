import React, { useEffect, useState } from "react";
import { Row, Col, Card, Container, Spinner, Alert, Button, Modal, Form } from "react-bootstrap";
import AnalizeService from "../../services/AnalizeService"; 

// Komponenta za pojedinu Analizu
const AnalizaCard = ({ analiza }) => {
  return (
    <Col md={6} lg={4} className="mb-3">
      <Card>
        <Card.Body>
          <Card.Title>Analiza</Card.Title>
          <Card.Subtitle className="mb-2 text-muted">
            Datum analize: {analiza.datum}
          </Card.Subtitle>

          <Row>
            <Col>
              <p><strong>pH:</strong> {analiza.pHVrijednost}</p>
              <p><strong>Fosfor:</strong> {analiza.fosfor}</p>
              <p><strong>Kalij:</strong> {analiza.kalij}</p>
              <p><strong>Magnezij:</strong> {analiza.magnezij}</p>
              <p><strong>Karbonati:</strong> {analiza.karbonati}</p>
              <p><strong>Humus:</strong> {analiza.humus}</p>
            </Col>
            <Col>
              <p><strong>Masa uzorka (g):</strong> {analiza.masaUzorka}</p>
              <p><strong>Vrsta tla:</strong> {analiza.vrstaTla}</p>
              <p><strong>Datum uzorka:</strong> {analiza.datumUzorka}</p>
              <p><strong>Mjesto uzorkovanja:</strong> {analiza.mjestoUzorkovanja}</p>
            </Col>
          </Row>

          <hr />
          <p><strong>Analitičar:</strong> {analiza.ime} {analiza.prezime}</p>
          <p><strong>Kontakt:</strong> {analiza.kontakt}</p>
          <p><strong>Stručna sprema:</strong> {analiza.strucnaSprema}</p>
        </Card.Body>
      </Card>
    </Col>
  );
};

// Glavna komponenta za prikaz svih analiza i dodavanje novih
const AnalizeGrid = () => {
  const [analize, setAnalize] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [showModal, setShowModal] = useState(false);
  const [novaAnaliza, setNovaAnaliza] = useState({
    pHVrijednost: "",
    fosfor: "",
    kalij: "",
    magnezij: "",
    karbonati: "",
    humus: "",
    masaUzorka: "",
    vrstaTla: "",
    datumUzorka: "",
    datum: "",
    ime: "",
    prezime: "",
    kontakt: "",
    strucnaSprema: ""
  });

  useEffect(() => {
    fetchAnalize();
  }, []);

  const fetchAnalize = async () => {
    try {
      const res = await AnalizeService.get(); 
      console.log("Podaci sa servera:", res); // <--- Ovdje vidiš što vraća backend
      setAnalize(res || []);
      setLoading(false);
    } catch (err) {
      setError(err.message || "Greška prilikom dohvaćanja analiza");
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    setNovaAnaliza({ ...novaAnaliza, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
  e.preventDefault();

  // Transformacija podataka u odgovarajuće tipove
  const payload = {
  datum: novaAnaliza.datum,
  pHVrijednost: parseFloat(novaAnaliza.pHVrijednost),
  fosfor: parseFloat(novaAnaliza.fosfor),
  kalij: parseFloat(novaAnaliza.kalij),
  magnezij: parseFloat(novaAnaliza.magnezij),
  karbonati: parseFloat(novaAnaliza.karbonati),
  humus: parseFloat(novaAnaliza.humus),
  masaUzorka: parseFloat(novaAnaliza.masaUzorka),
  vrstaTla: novaAnaliza.vrstaTla,
  datumUzorka: novaAnaliza.datumUzorka,
  mjestoUzorkovanja: novaAnaliza.mjestoUzorkovanja,
  ime: novaAnaliza.ime,
  prezime: novaAnaliza.prezime,
  kontakt: novaAnaliza.kontakt,
  strucnaSprema: novaAnaliza.strucnaSprema
};



  try {
    await AnalizeService.dodaj(payload); // umjesto axios.post
    await fetchAnalize(); 

    setShowModal(false);
    setNovaAnaliza({
      pHVrijednost: "",
      fosfor: "",
      kalij: "",
      magnezij: "",
      karbonati: "",
      humus: "",
      masaUzorka: "",
      vrstaTla: "",
      datumUzorka: "",
      datum: "",
      ime: "",
      prezime: "",
      kontakt: "",
      strucnaSprema: "",
      mjestoUzorkovanja: ""
    });
  } catch (err) {
    alert("Greška pri dodavanju nove analize");
    console.error(err);
  }
};


  if (loading) return <Spinner animation="border" className="m-5" />;
  if (error) return <Alert variant="danger" className="m-5">{error}</Alert>;

  return (
    <Container className="mt-4">
      <h2 className="mb-4">Popis analiza</h2>
      <Button className="mb-3" onClick={() => setShowModal(true)}>Dodaj novu analizu</Button>

      <Row>
      {analize?.map(a => (
        <AnalizaCard key={a.sifra} analiza={a} />
      ))}
    </Row>

      {/* Modal za dodavanje nove analize */}
      <Modal show={showModal} onHide={() => setShowModal(false)} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Dodaj novu analizu</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={handleSubmit}>
            <Row>
              <Col md={6}>
                <Form.Group className="mb-2">
                  <Form.Label>pH</Form.Label>
                  <Form.Control name="pHVrijednost" value={novaAnaliza.pHVrijednost || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Fosfor</Form.Label>
                  <Form.Control name="fosfor" value={novaAnaliza.fosfor || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Kalij</Form.Label>
                  <Form.Control name="kalij" value={novaAnaliza.kalij || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Magnezij</Form.Label>
                  <Form.Control name="magnezij" value={novaAnaliza.magnezij || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Karbonati</Form.Label>
                  <Form.Control name="karbonati" value={novaAnaliza.karbonati || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Humus</Form.Label>
                  <Form.Control name="humus" value={novaAnaliza.humus || ""} onChange={handleChange} />
                </Form.Group>
              </Col>
              <Col md={6}>
                <Form.Group className="mb-2">
                  <Form.Label>Masa uzorka (g)</Form.Label>
                  <Form.Control name="masaUzorka" value={novaAnaliza.masaUzorka || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Vrsta tla</Form.Label>
                  <Form.Control name="vrstaTla" value={novaAnaliza.vrstaTla || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Datum uzorka</Form.Label>
                  <Form.Control type="date" name="datumUzorka" value={novaAnaliza.datumUzorka || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Datum analize</Form.Label>
                  <Form.Control type="date" name="datum" value={novaAnaliza.datum || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Ime analitičara</Form.Label>
                  <Form.Control name="ime" value={novaAnaliza.ime || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Prezime analitičara</Form.Label>
                  <Form.Control name="prezime" value={novaAnaliza.prezime || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Kontakt</Form.Label>
                  <Form.Control name="kontakt" value={novaAnaliza.kontakt || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                  <Form.Label>Stručna sprema</Form.Label>
                  <Form.Control name="strucnaSprema" value={novaAnaliza.strucnaSprema || ""} onChange={handleChange} />
                </Form.Group>
                <Form.Group className="mb-2">
                <Form.Label>Mjesto uzorkovanja</Form.Label>
                <Form.Control 
                    name="mjestoUzorkovanja" 
                    value={novaAnaliza.mjestoUzorkovanja || ""} 
                    onChange={handleChange} 
                />
                </Form.Group>
              </Col>
            </Row>
            <Button type="submit" className="mt-3">Spremi analizu</Button>
          </Form>
        </Modal.Body>
      </Modal>
    </Container>
  );
};

export default AnalizeGrid;
