import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import AnaliticariService from "../../services/AnaliticariService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import { toast } from "react-toastify";
import { BACKEND_URL } from "../../constants";
import useLoading from "../../hooks/useLoading";
import { Col, Form, Pagination } from "react-bootstrap";


export default function AnaliticariPregled () {

    const [analiticar, setAnaliticari] = useState([]);
    const [stranica, setStranica] = useState(1);
    const [uvjet, setUvjet] = useState('');
    const { showLoading, hideLoading } = useLoading();
    const [ukupnoStranica, setUkupnoStranica] = useState(1);

    const navigate = useNavigate();

    async function dohvatiAnaliticari() {
    showLoading();
    let odgovor = await AnaliticariService.getStranicenje(stranica, uvjet);
    hideLoading();

    if (!odgovor) {
        setAnaliticari([]);
        return;
    }

    // Ako backend vraća { lista: [...], ukupnoStranica: X }
    if (Array.isArray(odgovor.lista)) {
        setAnaliticari(odgovor.lista);
        setUkupnoStranica(odgovor.ukupnoStranica || 1);
    } else if (Array.isArray(odgovor)) {
        setAnaliticari(odgovor);
    } else {
        setAnaliticari([]);
    }
}



    useEffect(()=>{
        dohvatiAnaliticari();

    },[stranica, uvjet]);


    function obrisi(sifra) {
    
        if(!confirm('Sigurno obrisati')) {
            return;
    
        }
        brisanje(sifra);
        }

    async function promijeniSliku(id, base64String) {
    try {
    const response = await fetch(`${BACKEND_URL}/api/v1/analiticar/${id}/slika`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ base64: base64String }) // mora biti 'base64', ne 'Base64'
    });

    const data = await response.json();

    if (response.ok) {
      toast.success("Slika uspješno promijenjena!");
      dohvatiAnaliticari();
    } else {
      toast.error(data.poruka || "Greška pri promjeni slike");
    }
  } catch (err) {
    console.error(err);
    toast.error("Neočekivana greška prilikom slanja slike");
  }
}


function handleFileChange(event, id) {
  const file = event.target.files[0];
  const reader = new FileReader();
  reader.onloadend = () => {
    const base64String = reader.result.split(",")[1]; // ukloni data:image/...;Base64,
    promijeniSliku(id, base64String);
  };
  reader.readAsDataURL(file);
}




    async function brisanje(sifra) {
    try {
      const odgovor = await AnaliticariService.obrisi(sifra);

      if (odgovor.greska) {
        toast.error(odgovor.poruka || "Ne možete obrisati ovog analitičara jer je povezan s drugim podacima.");
        
      } else {
        toast.success(odgovor.poruka || "Analitičar je uspješno obrisan.");
        
        dohvatiAnaliticari();
      }
    } catch (err) {
      console.error("Greška kod brisanja:", err);
      toast.error("Dogodila se neočekivana greška");
      
    }
  }

  function promjeniUvjet(e) {
        if(e.nativeEvent.key == "Enter"){
            console.log('Enter')
            setStranica(1);
            setUvjet(e.nativeEvent.srcElement.value);
            setAnaliticari([]);
        }
    }

    function povecajStranicu() {
    if(stranica < ukupnoStranica) setStranica(stranica + 1);
    }

    function smanjiStranicu() {
        if(stranica > 1) setStranica(stranica - 1);
    }




return (
  <>
    
           
                <Col key={1} sm={12} lg={4} md={4}>
                    <Form.Control
                    type='text'
                    name='trazilica'
                    placeholder='Dio imena i prezimena [Enter]'
                    maxLength={255}
                    defaultValue=''
                    onKeyUp={promjeniUvjet}
                    />
                </Col>
                <Col key={2} sm={12} lg={4} md={4}>
                    {analiticar && analiticar.length > 0 && (
                            <div style={{ display: "flex", justifyContent: "center" }}>
                               {/*  <Pagination size="lg">
                                <Pagination.Prev onClick={smanjiStranicu} />
                                <Pagination.Item disabled>{stranica}</Pagination.Item> 
                                <Pagination.Next
                                    onClick={povecajStranicu}
                                />
                            </Pagination> */}
                        </div>
                    )}
                </Col>
                <Col key={3} sm={12} lg={4} md={4}></Col>
    

    <Link
      className="btn btn-success"
      to={RouteNames.ANALITICARI_NOVI}
    >
      Dodavanje novog analitičara
    </Link>

   <Table striped bordered hover responsive>
  <thead>
    <tr>
      <th>Slika</th>
      <th>Ime</th>
      <th>Prezime</th>
      <th>Kontakt</th>
      <th>Stručna Sprema</th>
      <th>Akcija</th>
    </tr>
  </thead>

  <tbody>
    {Array.isArray(analiticar) && analiticar.map((a, index) => (
      <tr key={index}>
        <td>
          {a.slikaUrl ? (
            <img 
              src={a.slikaUrl ? `${BACKEND_URL}${a.slikaUrl}` : '/default-avatar.png'} 
              alt={`${a.ime} ${a.prezime}`} 
              style={{ width: "50px", height: "50px", objectFit: "cover", borderRadius: "50%" }}
            />
          ) : (
            <span>Nema slike</span>
          )}
          <br />
          <Button
            size="sm"
            style={{ marginTop: "5px" }}
            onClick={() => document.getElementById(`file-${a.sifra}`).click()}
          >
            Promijeni sliku
          </Button>
          <input
            type="file"
            id={`file-${a.sifra}`}
            style={{ display: "none" }}
            accept="image/*"
            onChange={(e) => handleFileChange(e, a.sifra)}
          />
        </td>
        <td>{a.ime}</td>
        <td>{a.prezime}</td>
        <td>{a.kontakt}</td>
        <td>{a.strucnaSprema}</td>
        <td>
          <Button onClick={() => navigate(`/analiticar/${a.sifra}`)}>
            Promjena
          </Button>
          &nbsp;&nbsp;&nbsp;&nbsp;
          <Button variant="danger" onClick={() => obrisi(a.sifra)}>
            Obriši
          </Button>
        </td>
      </tr>
    ))}
  </tbody>
</Table>

{/* PAGINACIJA NA DNU */}
{analiticar && analiticar.length > 0 && (
  <div style={{ display: "flex", justifyContent: "center", marginTop: "20px" }}>
    <Pagination size="lg">
      <Pagination.Prev onClick={smanjiStranicu} disabled={stranica === 1} />
      <Pagination.Item active>{stranica}</Pagination.Item>
      <Pagination.Next onClick={povecajStranicu} disabled={stranica === ukupnoStranica} />
    </Pagination>
  </div>
)}
</>
);
}

