import { HttpService } from "./HttpService"; // pretpostavljam da HttpService radi axios instancu s baznim URL-om

const AnalizeService = {
  // Dohvati sve analize
  get: async () => {
    try {
      const response = await HttpService.get("/analiza");
      return response.data;
    } catch (error) {
      console.error("Greška pri dohvaćanju analiza:", error);
      throw error;
    }
  },

  // Dohvati jednu analizu po šifri
  getBySifra: async (sifra) => {
    try {
      const response = await HttpService.get(`/analiza/${sifra}`);
      return response.data;
    } catch (error) {
      console.error(`Greška pri dohvaćanju analize ${sifra}:`, error);
      throw error;
    }
  },

  // Kreiraj novu analizu
  dodaj: async (analiza) => {
    try {
      const response = await HttpService.post("/analiza", analiza);
      return response.data;
    } catch (error) {
      console.error("Greška pri dodavanju analize:", error);
      throw error;
    }
  },

  // Promijeni postojeću analizu
  promjeni: async (sifra, analiza) => {
    try {
      const response = await HttpService.put(`/analiza/${sifra}`, analiza);
      return response.data;
    } catch (error) {
      console.error(`Greška pri promjeni analize ${sifra}:`, error);
      throw error;
    }
  },

  // Obriši analizu
  obrisi: async (sifra) => {
    try {
      const odgovor = await HttpService.delete(`/analiza/${sifra}`);
      return odgovor.data; 
    } catch (e) {
      if (e.response && e.response.data) {
        return e.response.data; 
      }
      throw e;
    }
  }
};



export default AnalizeService;
