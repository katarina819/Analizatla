import {HttpService} from "./HttpService"

async function get() {

    return await HttpService.get('/Lokacija')
    .then((odgovor)=> {

        /* console.log(odgovor.data) */
        return odgovor.data

    })
    .catch((e)=>{
        console.error(e);
    }) 


}


async function getBySifra(sifra) {

    return await HttpService.get('/Lokacija/' + sifra)
    .then((odgovor)=> {

        /* console.log(odgovor.data) */
        return odgovor.data

    })
    .catch((e)=>{
        console.error(e);
    }) 


}

async function dodaj(lokacija) {
  return await HttpService.post('/Lokacija', lokacija)
    .then((odgovor) => odgovor.data) 
    .catch((e) => null);
}

async function obrisi(sifra) {
  try {
    const odgovor = await HttpService.delete('/Lokacija/' +sifra);
    return odgovor.data; 
  } catch (e) {
    if (e.response && e.response.data) {
      return e.response.data; 
    }
    throw e; 
  }
}

async function promjeni(sifra, lokacija) {

    return await HttpService.put('/Lokacija/' +sifra, lokacija)
    .then((odgovor) => {return true;})
    .catch((e) => {return false;});
}


export default {
    get,
    getBySifra,
    dodaj,
    obrisi,
    promjeni
};