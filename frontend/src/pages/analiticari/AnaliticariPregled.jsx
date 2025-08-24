import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import AnaliticariService from "../../services/AnaliticariService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import { toast } from "react-toastify";
import { BACKEND_URL } from "../../constants";


export default function AnaliticariPregled () {

    const [analiticar, setAnaliticari] = useState([]);
    
    const navigate = useNavigate();

    async function dohvatiAnaliticari() {
       const odgovor = await AnaliticariService.get()
       console.log("Analiticari response:", odgovor);
       setAnaliticari(odgovor);

    }

    useEffect(()=>{
        dohvatiAnaliticari();

    },[])


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


return (
  <>

    

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
    {analiticar && analiticar.map((a, index) => (
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

  </>
)
}
