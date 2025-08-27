import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Form, Row, Col } from "react-bootstrap";
import LokacijeService from "../../services/LokacijeService"; // provjeri putanju

export default function LokacijeDodaj() {
  const [unos, setUnos] = useState("");
  const navigate = useNavigate();

  async function dodajLokaciju() {
    if (!unos) return;

    // 1. Dodaj lokaciju u backend
    await LokacijeService.dodaj({ mjestoUzorkovanja: unos });

    // 2. Nakon dodavanja idi na nadzornu ploču
    navigate("/nadzornaploca");
  }

  function odradiSubmit(e) {
    e.preventDefault();
    dodajLokaciju();
  }

  return (
    <Form onSubmit={odradiSubmit} style={{ marginBottom: "20px" }}>
      <Form.Group controlId="mjestoUzorkovanja">
        <Form.Label>Naziv lokacije</Form.Label>
        <Form.Control
          type="text"
          value={unos}
          onChange={(e) => setUnos(e.target.value)}
          placeholder="Unesite naziv mjesta"
          required
        />
      </Form.Group>

      <Row style={{ marginTop: "10px" }}>
        <Col>
          <Button variant="success" type="submit">
            Dodaj lokaciju
          </Button>
        </Col>
      </Row>
    </Form>
  );
}
