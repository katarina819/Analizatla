import { useEffect, useState } from "react";
import { Container, Table } from "react-bootstrap";
import LokacijeService from "../../services/LokacijeService";



export default function LokacijePregled () {

    const [lokacija, setLokacije] = useState([]);

    async function dohvatiLokacije() {
       const odgovor = await LokacijeService.get()
       setLokacije(odgovor)

    }

    useEffect(()=>{
        dohvatiLokacije();

    },[])

    return (
        <>
        Tablični Pregled lokacija
        <Table striped bordered hover responsive>
            <thead>
                <tr>
                    <th>Mjesto uzorkovanja</th>

                </tr>

            </thead>
            <tbody>
                {lokacija && lokacija.map((lokacija, index)=>(
                    <tr key={index}>
                        <td>
                           {lokacija.mjestoUzorkovanja} 
                        </td>
                    </tr>

                ))}
            </tbody>

        </Table>
        </>
    )
}