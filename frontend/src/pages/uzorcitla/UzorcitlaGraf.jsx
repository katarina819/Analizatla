import React, { useEffect, useState } from "react";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer
} from "recharts";
import UzorcitlaService from "../../services/UzorcitlaService"; // tvoj API servis

export default function UzorcitlaGraf() {
  const [grafDataParsed, setGrafDataParsed] = useState([]);

  useEffect(() => {
  async function fetchData() {
    try {
      const data = await UzorcitlaService.get();
      const parsed = data.map(item => ({
  ...item,
  datumUzorka: item.datum ? new Date(item.datum).getTime() : null,      // **datum uzorka**
  datum: item.datumUzorka ? new Date(item.datumUzorka).getTime() : null // **datum analize**
}));

      setGrafDataParsed(parsed);
    } catch (err) {
      console.error("Greška kod dohvaćanja podataka za graf:", err);
    }
  }
  fetchData();
}, []);

  function formatirajDatum(datumISO) {
    if (!datumISO) return "";
    const d = new Date(datumISO);
    const dan = String(d.getDate()).padStart(2, "0");
    const mjesec = String(d.getMonth() + 1).padStart(2, "0");
    const godina = d.getFullYear();
    return `${dan}-${mjesec}-${godina}`;
  }

  return (
    <div style={{ marginTop: "100px" }}>
      <h4>📊 Usporedba datuma analize i datuma uzorka po lokacijama</h4>
      <ResponsiveContainer width="100%" height={400}>
        <BarChart data={grafDataParsed}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="mjestoUzorkovanja" tick={{ fontSize: 12 }} />
          <YAxis
            domain={["auto", "auto"]}
            tickFormatter={(time) => formatirajDatum(time)}
          />
          <Tooltip
            labelFormatter={(mjesto) => `Lokacija: ${mjesto}`}
            formatter={(val, name) => [formatirajDatum(val), name]}
          />
          <Legend />
          
        <Bar dataKey="datumUzorka" fill="#82ca9d" name="Datum uzorkovanja" />

        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}
