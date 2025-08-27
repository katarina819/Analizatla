import { Container, Row, Col, Card } from "react-bootstrap";
import CountUp from "react-countup";
import slika from '../assets/edunovaSlika.png';

export default function Pocetna() {
    return (
        <Container className="text-center mt-5">
            <h1 className="mb-3" style={{ fontWeight: "700", color: "#2c3e50" }}>
                Dobrodošli!
            </h1>
            <hr style={{ width: "60px", borderTop: "3px solid #2ecc71", margin: "0 auto 40px" }} />

            <img 
                src={slika} 
                alt="Edunova" 
                style={{maxWidth: 400, borderRadius: "15px", boxShadow: "0 8px 20px rgba(0,0,0,0.2)"}}
                className="img-fluid mb-5"
            />

            <h3 className="mb-3" style={{ color: "#34495e" }}>Nudimo usluge:</h3>
            <p style={{fontSize: "1.2rem", maxWidth: "600px", margin: "0 auto 40px"}}>
                Određivanje <b>pH</b>, <b>fosfora</b>, <b>kalija</b>, <b>magnezija</b>, <b>karbonata</b> i <b>humusa</b> u tlu
            </p>

            <Row className="justify-content-center">
                <Col md={6} lg={4}>
                    <Card 
                        className="text-center p-4 shadow-sm"
                        style={{ borderRadius: "15px", transition: "transform 0.3s", cursor: "pointer" }}
                        onMouseEnter={e => e.currentTarget.style.transform = "translateY(-10px)"}
                        onMouseLeave={e => e.currentTarget.style.transform = "translateY(0)"}
                    >
                        <Card.Body>
                            <h5 style={{ color: "#27ae60", fontWeight: "600" }}>Do sada smo proveli:</h5>
                            <h2 style={{ color: "#27ae60", fontWeight: "bold" }}>
                                <CountUp end={10} duration={15} /> analiza
                            </h2>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
}
