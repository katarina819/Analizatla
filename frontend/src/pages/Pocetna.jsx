import { Container } from "react-bootstrap";
import slika from '../assets/edunovaSlika.png'


export default function Pocetna() {

    return (
        
            <>
            Dobrodošli <hr />
            <img src={slika} style={{maxWidth: 600, border: '2px solid red'}} />
            </>
        
    )
}