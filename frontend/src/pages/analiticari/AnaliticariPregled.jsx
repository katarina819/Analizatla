import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import AnaliticariService from "../../services/AnaliticariService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import { toast } from "react-toastify";



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
