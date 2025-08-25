import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import UzorcitlaService from "../../services/UzorcitlaService";
import { Link, useNavigate } from "react-router-dom";
import { RouteNames } from "../../constants";
import { toast } from "react-toastify";
// import UzorcitlaGraf from "./UzorcitlaGraf";


export default function UzorcitlaPregled () {

    const [uzorcitla, setUzorciTla] = useState([]);
    
    const navigate = useNavigate();

    async function dohvatiUzorciTla() {
    const odgovor = await UzorcitlaService.get();
    console.log(odgovor); // cijeli objekt
    console.log(odgovor.map(u => u.sifra)); // vidi koje polje je ID
    setUzorciTla(odgovor);
}


    useEffect(()=>{
        dohvatiUzorciTla();

    },[])

    function formatirajDatum(datumISO) {
    if (!datumISO) return ""; // ako je null ili undefined

    const d = new Date(datumISO);
    const dan = String(d.getDate()).padStart(2, "0");
    const mjesec = String(d.getMonth() + 1).padStart(2, "0"); // mjeseci idu od 0
    const godina = d.getFullYear();

    return `${dan}-${mjesec}-${godina}`;
    }


    function obrisi(sifra) {

        if(!confirm('Sigurno obrisati')) {
            return;

        }
        brisanje(sifra);
    }

    async function brisanje(sifra) {
    try {
      const odgovor = await UzorcitlaService.obrisi(sifra);

      if (odgovor.greska) {
        toast.error(odgovor.poruka || "Ne možete obrisati ovaj uzorak jer je povezan s drugim podacima.");
      } else {
        toast.success(odgovor.poruka || "Uzorak je uspješno obrisan.");
        dohvatiUzorciTla();
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
         to={RouteNames.UZORCITLA_NOVI}
       >
         Dodavanje novog uzorka tla
       </Link>
   
       <Table striped bordered hover responsive>
         <thead>
           <tr>
             <th>Masa uzorka (g) </th>
             <th>Vrsta tla</th>
             <th>Datum</th>
             <th>Lokacija</th>
             <th>Akcija</th>
           </tr>
         </thead>
   
         <tbody>
           {uzorcitla && uzorcitla.map((u, index) => (
             <tr key={index}>
               <td>{u.masaUzorka}</td>
               <td>{u.vrstaTla}</td>
               <td>{formatirajDatum(u.datum)}</td>
               <td>{u.mjestoUzorkovanja}</td>
               <td>
                 <Button onClick={() => navigate(`/uzorcitla/${u.sifra}`)}>
                   Promjena
                 </Button>
                 &nbsp;&nbsp;&nbsp;&nbsp;
                 <Button variant="danger" onClick={() => obrisi(u.sifra)}>
                   Obriši
                 </Button>
               </td>
             </tr>
           ))}
         </tbody>
       </Table>
       
       {/* <UzorcitlaGraf /> */}
     </>
   )
   }
   