import { Container } from "react-bootstrap";
import CountUp from "react-countup";
import slika from '../assets/edunovaSlika.png';

export default function Pocetna() {
    return (
        <Container className="text-center mt-4">
            <h1 className="mb-3">Dobrodošli!</h1>
            <hr />

            <img 
                src={slika} 
                alt="Edunova" 
                style={{maxWidth: 600, border: '2px solid red', borderRadius: 10}}
                className="img-fluid mb-4"
            />

            <h3 className="mt-4">Nudimo usluge:</h3>
            <p style={{fontSize: "1.2rem"}}>
                određivanje <b>pH</b>, <b>fosfora</b>, <b>kalija</b>, <b>magnezija</b>, <b>karbonata</b>, <b>humusa</b>
            </p>

            <div style={{
                backgroundColor: "#f8f9fa",
                padding: "20px",
                borderRadius: "12px",
                display: "inline-block",
                marginTop: "20px",
                boxShadow: "0 4px 12px rgba(0,0,0,0.1)"
            }}>
                <h4>Do sada smo proveli:</h4>
                <h2 style={{color: "green", fontWeight: "bold"}}>
                    <CountUp end={10} duration={15} /> analiza
                </h2>
            </div>
        </Container>
    );
}
