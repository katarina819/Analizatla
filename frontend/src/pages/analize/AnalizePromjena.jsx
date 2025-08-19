import { Button, Col, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import AnalizeService from "../../services/AnalizeService";
import { RouteNames } from "../../constants";

export default function AnalizePromjeni() {
    const navigate = useNavigate();
    const params = useParams();

    // funkcija za konverziju datuma u yyyy-MM-dd
    function toInputDate(d) {
    if (!d) return "";
    const date = new Date(d);
    if (isNaN(date)) return "";
    return date.toISOString().split("T")[0]; // yyyy-MM-dd
}


    const [analiza, setAnaliza] = useState({
        MasaUzorka: "",
        VrstaTla: "",
        Datum: "",
        MjestoUzorkovanja: "",
        pHVrijednost: "",
        Fosfor: "",
        Kalij: "",
        Magnezij: "",
        Karbonati: "",
        Humus: "",
        Ime: "",
        Prezime: "",
        Kontakt: "",
        StrucnaSprema: "",
        DatumAnalize: "",
    });

    useEffect(() => {
        async function ucitajAnaliza() {
            const odgovor = await AnalizeService.getBySifra(params.sifra);

            const mapped = {
                MasaUzorka: odgovor.masaUzorka ?? "",
                VrstaTla: odgovor.vrstaTla ?? "",
                Datum: toInputDate(odgovor.datumUzorka),
                MjestoUzorkovanja: odgovor.mjestoUzorkovanja ?? "",
                pHVrijednost: (() => {
                    const ph = odgovor.pHVrijednost;
                    return ph !== null && ph !== undefined
                        ? parseFloat(ph.toString().replace(",", "."))
                        : "";
                })(),
                Fosfor: odgovor.fosfor ?? "",
                Kalij: odgovor.kalij ?? "",
                Magnezij: odgovor.magnezij ?? "",
                Karbonati: odgovor.karbonati ?? "",
                Humus: odgovor.humus ?? "",
                Ime: odgovor.ime ?? "",
                Prezime: odgovor.prezime ?? "",
                Kontakt: odgovor.kontakt ?? "",
                StrucnaSprema: odgovor.strucnaSprema ?? "",
                DatumAnalize: toInputDate(odgovor.datum),
            };

            setAnaliza(mapped);
        }

        ucitajAnaliza();
    }, [params.sifra]);

    async function promjeni(sifra, analiza) {
        await AnalizeService.promjeni(sifra, analiza);
        navigate(RouteNames.ANALIZA_PREGLED);
    }

    function odradiSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);

    // privremeni objekt iz forme
    const novaAnaliza = {};
    polja.forEach(f => {
    let value = podaci.get(f.key);
    if (f.type === "number" && value !== "") {
        // zamijeni zarez s točkom prije parseFloat
        value = parseFloat(value.toString().replace(",", "."));
    }
    novaAnaliza[f.key] = value;
});

    // mapiranje na backend JSON
    const backendAnaliza = {
        sifra: parseInt(params.sifra),
        datum: analiza.DatumAnalize ? new Date(analiza.DatumAnalize).toISOString() : null,
        pHVrijednost: analiza.pHVrijednost || 0,
        fosfor: analiza.Fosfor || 0,
        kalij: analiza.Kalij || 0,
        magnezij: analiza.Magnezij || 0,
        karbonati: analiza.Karbonati || 0,
        humus: analiza.Humus || 0,
        masaUzorka: analiza.MasaUzorka || 0,
        vrstaTla: analiza.VrstaTla || "",
        datumUzorka: analiza.Datum ? new Date(analiza.Datum).toISOString() : null,
        mjestoUzorkovanja: analiza.MjestoUzorkovanja || "",
        ime: analiza.Ime || "",
        prezime: analiza.Prezime || "",
        kontakt: analiza.Kontakt || "",
        strucnaSprema: analiza.StrucnaSprema || ""
    };

    promjeni(params.sifra, backendAnaliza);
}



    const polja = [
        { label: "Masa uzorka", key: "MasaUzorka", type: "number", step: "0.01" },
        { label: "Vrsta tla", key: "VrstaTla", type: "text" },
        { label: "Datum uzorka", key: "Datum", type: "date" },
        { label: "Mjesto uzorkovanja", key: "MjestoUzorkovanja", type: "text" },
        { label: "pH", key: "pHVrijednost", type: "number", step: "0.01" },
        { label: "Fosfor", key: "Fosfor", type: "number", step: "0.01" },
        { label: "Kalij", key: "Kalij", type: "number", step: "0.01" },
        { label: "Magnezij", key: "Magnezij", type: "number", step: "0.01" },
        { label: "Karbonati", key: "Karbonati", type: "number", step: "0.01" },
        { label: "Humus", key: "Humus", type: "number", step: "0.01" },
        { label: "Analitičar ime", key: "Ime", type: "text" },
        { label: "Analitičar prezime", key: "Prezime", type: "text" },
        { label: "Kontakt", key: "Kontakt", type: "text" },
        { label: "Stručna sprema", key: "StrucnaSprema", type: "text" },
        { label: "Datum analize", key: "DatumAnalize", type: "date" },
    ];

    return (
        <>
            <h3>Promjeni analizu</h3>
            <Form onSubmit={odradiSubmit}>
    {polja.map(f => (
        <Form.Group key={f.key} controlId={f.key} className="mb-3">
            <Form.Label>{f.label}</Form.Label>
            <Form.Control
                type={f.type}
                name={f.key}
                step={f.step}
                required
                value={
                    f.type === "date"
                        ? toInputDate(analiza[f.key])
                        : analiza[f.key] !== null && analiza[f.key] !== undefined
                        ? analiza[f.key]
                        : ""
                }
                onChange={e => {
                    let val = e.target.value;
                    if (f.type === "number") {
                        val = val === "" ? "" : parseFloat(val.toString().replace(",", "."));
                    }
                    setAnaliza({ ...analiza, [f.key]: val });
                }}

            />

        </Form.Group>
    ))}

    <hr style={{ marginTop: "50px" }} />
    <Row>
        <Col xs={6} sm={6} md={3} lg={2} xl={6} xxl={6}>
            <Link to={RouteNames.ANALIZA_PREGLED} className="btn btn-danger">
                Odustani
            </Link>
        </Col>
        <Col xs={6} sm={6} md={9} lg={10} xl={6} xxl={6}>
            <Button variant="success" type="submit">
                Promjeni analizu
            </Button>
        </Col>
    </Row>
</Form>

        </>
    );
}
