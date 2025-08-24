import React from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import 'leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css';
import 'leaflet-defaulticon-compatibility';

const lokacije = [
  { ime: 'Osijek', lat: 45.5511, lng: 18.6939 },
  { ime: 'Vinkovci', lat: 45.2885, lng: 18.8205 },
  { ime: 'Našice', lat: 45.4869, lng: 18.0916 },
  { ime: 'Đakovo', lat: 45.3083, lng: 18.4142 },
  { ime: 'Slavonski Brod', lat: 45.1600, lng: 18.0153 },
  { ime: 'Vukovar', lat: 45.3500, lng: 18.9969 },
  { ime: 'Ilok', lat: 45.2158, lng: 19.3550 },
  { ime: 'Valpovo', lat: 45.6267, lng: 18.1094 },
  { ime: 'Beli Manastir', lat: 45.7761, lng: 18.6633 },
  { ime: 'Županja', lat: 45.1128, lng: 18.7133 }
];

const KartaHrvatske = () => {
  return (
    <MapContainer center={[45.1, 18.6]} zoom={8} style={{ height: '600px', width: '100%' }}>
      <TileLayer
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution="&copy; OpenStreetMap contributors"
      />
      {lokacije.map((lok, idx) => (
        <Marker key={idx} position={[lok.lat, lok.lng]}>
          <Popup>{lok.ime}</Popup>
        </Marker>
      ))}
    </MapContainer>
  );
};

export default KartaHrvatske;
