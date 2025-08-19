import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import AnaliticariService from "../../services/AnaliticariService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";



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
        const odgovor= await AnaliticariService.obrisi(sifra);
        dohvatiAnaliticari();
           
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
