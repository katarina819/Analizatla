# Web aplikacija za Analizu Tla

Ova web aplikacija omogućuje upravljanje i pregled podataka vezanih uz analizu tla. Korisnici se mogu prijaviti putem tokena, pregledavati i upravljati lokacijama uzorkovanja, analitičarima, analizama i podacima o tlu. Aplikacija također pruža pristup Swagger dokumentaciji i ERA dijagramu baze podataka.

---

## Tehnologije

- **Frontend:** React 19, React Router DOM, React Bootstrap, Recharts, React Leaflet, Axios
- **Mapiranje:** Leaflet
- **UI & Styling:** Bootstrap 5
- **Notifikacije:** React Toastify
- **Build alat:** Vite
- **Linter:** ESLint

---

## Značajke

1. **Autentikacija**
   - Prijava korisnika putem tokena.
   - Zaštićene rute dostupne samo prijavljenim korisnicima.

2. **Lokacije uzorkovanja**
   - Pregled svih lokacija.
   - Dodavanje novih lokacija.
   - Promjena i brisanje postojećih lokacija.
   - Prikaz lokacija na karti (Leaflet).

3. **Analitičari**
   - Pregled svih analitičara.
   - Dodavanje, uređivanje i brisanje analitičara.

4. **Analize tla**
   - Pregled svih analiza.
   - Dodavanje novih analiza.
   - Promjena i brisanje postojećih analiza.
   - Detaljni podaci o tlu i uzorcima.

5. **Swagger dokumentacija**
   - Pristup Swagger sučelju za pregled API-ja.

6. **ERA dijagram**
   - Prikaz ER dijagrama baze podataka za pregled strukture podataka.

7. **Odjava**
   - Sigurno odjavljivanje korisnika.

---

## Pokretanje aplikacije

1. Klonirajte repozitorij:
```bash
git clone https://github.com/katarina819/Analizatla.git 
```

2. Instalirajte ovisnosti:
```bash
npm install
```

3. Pokrenite aplikaciju:
```bash
npm run dev
```

4. Otvorite preglednik i idite na:
```text
http://localhost:5177/
```