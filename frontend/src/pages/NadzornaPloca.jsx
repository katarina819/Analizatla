import React, { useState, useEffect } from "react";
import KartaHrvatske from "./KartaHrvatske";
import LokacijeService from "../services/LokacijeService";

const lokacijeKoordinate = {
  Osijek: { lat: 45.5511, lng: 18.6939 },
  Vinkovci: { lat: 45.2885, lng: 18.8205 },
  Našice: { lat: 45.4869, lng: 18.0916 },
  Đakovo: { lat: 45.3083, lng: 18.4142 },
  "Slavonski Brod": { lat: 45.1600, lng: 18.0153 },
  Vukovar: { lat: 45.3500, lng: 18.9969 },
  Ilok: { lat: 45.2158, lng: 19.3550 },
  Valpovo: { lat: 45.6267, lng: 18.1094 },
  "Beli Manastir": { lat: 45.7761, lng: 18.6633 },
  Županja: { lat: 45.1128, lng: 18.7133 },
};

// funkcija za dohvat koordinata preko Nominatim-a
async function dohvatiKoordinate(mjesto) {
  if (lokacijeKoordinate[mjesto]) return lokacijeKoordinate[mjesto];

  try {
    const response = await fetch(
      `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(mjesto + ", Hrvatska")}`
    );
    const data = await response.json();
    if (data && data.length > 0) {
      return { lat: parseFloat(data[0].lat), lng: parseFloat(data[0].lon) };
    }
  } catch (err) {
    console.error("Greška kod dohvaćanja koordinata:", err);
  }
  return { lat: 45.1, lng: 18.6 }; // fallback
}

const NadzornaPloca = () => {
  const [lokacije, setLokacije] = useState([]);

  useEffect(() => {
    async function dohvatiSve() {
      const odgovor = await LokacijeService.get();
      const lokacijeSaKoordinatama = await Promise.all(
        odgovor.map(async (lok) => {
          const coords = await dohvatiKoordinate(lok.mjestoUzorkovanja);
          return { ...lok, ...coords };
        })
      );
      setLokacije(lokacijeSaKoordinatama);
    }
    dohvatiSve();
  }, []);

  return (
    <div>
      <h1>Nadzorna ploča</h1>
      <KartaHrvatske lokacije={lokacije} />
    </div>
  );
};

export default NadzornaPloca;
