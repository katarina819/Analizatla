import { HttpService } from "./HttpService";
import LokacijaService from "./LokacijeService";

async function get() {
  return await HttpService.get('/Uzorcitla')
    .then((odgovor) => odgovor.data)
    .catch((e) => { console.error(e); });
}

async function getBySifra(sifra) {
  return await HttpService.get('/Uzorcitla/' + sifra)
    .then((odgovor) => odgovor.data)
    .catch((e) => { console.error(e); });
}

// Dodavanje uzorka sa automatskim lociranjem lokacije
async function dodaj({ masaUzorka, vrstaTla, datum, mjestoUzorkovanja }) {
  // 1. Dohvati sve lokacije
  const sveLokacije = await LokacijaService.get();
  let lokacija = sveLokacije.find(l => l.mjestoUzorkovanja === mjestoUzorkovanja);

  // 2. Ako ne postoji, dodaj novu
  if (!lokacija) {
  const kreiranaLokacija = await LokacijaService.dodaj({ mjestoUzorkovanja });
  if (!kreiranaLokacija) return false;
  lokacija = kreiranaLokacija; // sada imamo sifru
}

    // Ponovo dohvatimo lokaciju da dobijemo ID
    const sveLokacijeUpdate = await LokacijaService.get();
    lokacija = sveLokacijeUpdate.find(l => l.mjestoUzorkovanja === mjestoUzorkovanja);
  

  // 3. Kreiraj objekt za backend
  const noviUzorak = {
    masaUzorka,
    vrstaTla,
    datum,
    mjestoUzorkovanja,
  };

  return await HttpService.post('/Uzorcitla', noviUzorak)
    .then(() => true)
    .catch(() => false);
}

async function obrisi(sifra) {
  try {
    const odgovor = await HttpService.delete('/Uzorcitla/' + sifra);
    return odgovor.data; 
  } catch (e) {
    if (e.response && e.response.data) {
      return e.response.data; 
    }
    throw e; 
  }
}

async function promjeni(sifra, { masaUzorka, vrstaTla, datum, mjestoUzorkovanja }) {
  const dto = { masaUzorka, vrstaTla, datum, mjestoUzorkovanja };
  return await HttpService.put('/Uzorcitla/' + sifra, dto)
    .then(() => true)
    .catch(() => false);
}



export default {
  get,
  getBySifra,
  dodaj,
  obrisi,
  promjeni
};
