import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import LokacijeService from "../../services/LokacijeService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import { toast } from "react-toastify";



export default function LokacijePregled () {

    const [lokacija, setLokacije] = useState([]);
    
    const navigate = useNavigate();

    async function dohvatiLokacije() {
       const odgovor = await LokacijeService.get()
       setLokacije(odgovor)

    }

    useEffect(()=>{
        dohvatiLokacije();

    },[])

    function obrisi(sifra) {

        if(!confirm('Sigurno obrisati')) {
            return;

        }
        brisanje(sifra);
    }

    async function brisanje(sifra) {
    try {
      const odgovor = await LokacijeService.obrisi(sifra);

      if (odgovor.greska) {
        toast.error(odgovor.poruka || "Ne možete obrisati ovu lokaciju jer je povezana s drugim podacima.");
        
      } else {
        toast.success(odgovor.poruka || "Lokacija je uspješno obrisana.");
        
        dohvatiLokacije();
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
        to={RouteNames.SMJER_NOVI} >Dodavanje nove lokacije </Link>
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Mjesto uzorkovanja</th>
                    <th>Akcija</th>

                </tr>

            </thead>
            <tbody>
                {lokacija && lokacija.map((lokacija, index)=>(
                    <tr key={index}>
                        <td>
                           {lokacija.mjestoUzorkovanja} 
                        </td>
                        <td>

                            <Button 
                            onClick={()=>navigate(`/lokacija/${lokacija.sifra}`)}>
                                Promjena
                            </Button>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <Button variant="danger"
                            onClick={()=>obrisi(lokacija.sifra)}>
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