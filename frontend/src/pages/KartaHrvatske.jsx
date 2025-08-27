import React from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import 'leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css';
import 'leaflet-defaulticon-compatibility';

const KartaHrvatske = ({ lokacije }) => {
  return (
    <MapContainer center={[45.1, 18.6]} zoom={8} style={{ height: '600px', width: '100%' }}>
      <TileLayer
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution="&copy; OpenStreetMap contributors"
      />
      {lokacije
        .filter(lok => lok.lat && lok.lng)
        .map((lok, idx) => (
          <Marker key={idx} position={[lok.lat, lok.lng]}>
            <Popup>{lok.mjestoUzorkovanja}</Popup>
          </Marker>
        ))}
    </MapContainer>
  );
};

export default KartaHrvatske;
