import {HttpService} from "./HttpService"

async function get() {

    return await HttpService.get('/Analiticar')
    .then((odgovor)=> {

        /* console.log(odgovor.data) */
        return odgovor.data

    })
    .catch((e)=>{
        console.error(e);
    }) 


}


async function getBySifra(sifra) {

    return await HttpService.get('/Analiticar/' + sifra)
    .then((odgovor)=> {

        /* console.log(odgovor.data) */
        return odgovor.data

    })
    .catch((e)=>{
        console.error(e);
    }) 


}


async function dodaj(analiticar) {

    return await HttpService.post('/Analiticar', analiticar)
    .then((odgovor) => {return true;})
    .catch((e) => {return false;});
}

async function obrisi(sifra) {
  try {
    const odgovor = await HttpService.delete('/Analiticar/' +sifra);
    return odgovor.data; 
  } catch (e) {
    if (e.response && e.response.data) {
      return e.response.data; 
    }
    throw e; 
  }
}

async function promjeni(sifra, analiticar) {

    return await HttpService.put('/Analiticar/' +sifra, analiticar)
    .then((odgovor) => {return true;})
    .catch((e) => {return false;});
}

async function getStranicenje(stranica, uvjet) {
    try {
        const odgovor = await HttpService.get(`/Analiticar/traziStranicenje/${stranica}?uvjet=${uvjet}`);
        return odgovor.data;
    } catch (e) {
        if (e.response && e.response.data) {
            return e.response.data;
        }
        throw e;
    }
}



export default {
    get,
    getBySifra,
    dodaj,
    obrisi,
    promjeni,
    getStranicenje    
}