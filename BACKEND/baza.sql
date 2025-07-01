use master;
go
drop database if exists analiza_tla;
go
create database analiza_tla;
go
use analiza_tla;

create table lokacije(
sifra int not null primary key identity(1,1),
mjestouzorkovanja varchar (100) not null
);

create table uzorcitla(
sifra int not null primary key identity (1,1),
masauzorka decimal (18,2) not null,
vrstatla varchar (50) not null,
datum datetime,
lokacija int not null references lokacije(sifra)
);


create table analiticari(
sifra int not null primary key identity (1,1),
ime varchar (20) not null,
prezime varchar (20) not null,
kontakt varchar (50) not null,
strucnasprema varchar (100)
);

create table analize(
sifra int not null primary key identity(1,1),
datum datetime,
uzoraktla int not null references uzorcitla(sifra),
analiticar int not null references analiticari(sifra),
pHvrijednost decimal (4,2) not null,
fosfor decimal (5,2) not null,
kalij decimal (5,2) not null,
magnezij decimal (5,2) not null,
karbonati decimal (5,2) not null,
humus decimal (4,2) not null
);


insert into lokacije (mjestouzorkovanja)
values
('Osijek'),
('Vinkovci'),
('Našice'),
('Đakovo'),
('Slavonski Brod'),
('Vukovar'),
('Ilok'),
('Valpovo'),
('Beli Manastir'),
('Županja');

insert into uzorcitla(masauzorka, vrstatla, datum, lokacija)
values
(2.50, 'Glinasto tlo', '2025-04-01', 1),
(3.10, 'Pjeskovito tlo', '2025-04-02', 2),
(1.85, 'Ilovasto tlo', '2025-04-05', 3),
(2.00, 'Crvenica', '2025-04-10', 4),
(2.75, 'Crnica', '2025-04-12', 5),
(3.20, 'Humusno tlo', '2025-04-15', 6),
(2.10, 'Smeđe tlo', '2025-04-18', 7),
(1.95, 'Pjeskovito glinasto tlo','2025-04-20', 8),
(2.30, 'Sivo tlo', '2025-04-22', 9),
(2.80, 'Crnica','2025-04-25', 10);

insert into analiticari(ime, prezime, kontakt, strucnasprema)
values
('Ana', 'Kovačević', 'ana.kovacevic@email.com', 'Magistar agronomije'),
('Ivan', 'Horvat', 'ivan.horvat@email.com', 'Diplomirani inženjer kemije'),
('Petra', 'Marić', 'petra.maric@email.com', 'Magistar biologije'),
('Marko', 'Novak',  'marko.novak@email.com', 'Kemijski tehničar'),
('Lucija', 'Babić', 'lucija.babic@email.com', 'Poljoprivredni tehničar'),
('Filip', 'Jurić', 'filip.juric@email.com', 'Diplomirani inženjer agronomije'),
('Matea', 'Grgić', 'matea.grgic@email.com', 'Magistar kemije'),
('Tomislav', 'Perić', 'tomislav.peric@email.com', 'Poljoprivredni tehničar'),
('Ema', 'Tomić', 'ema.tomic@email.com', 'Agronomski tehničar'),
('Dario', 'Knežević', 'dario.knezevic@email.com', 'Diplomirani inženjer zaštite okoliša');

insert into analize(datum, analiticar, uzoraktla, pHvrijednost, fosfor, kalij, magnezij, karbonati, humus)
values
('2025-04-05', 1, 1, 5.75, 6.50, 15.20, 8.40, 4.50, 2.80),
('2025-04-06', 2, 2, 6.20, 10.30, 22.10, 9.20, 6.80, 3.10),
('2025-04-08', 3, 3, 5.90, 8.00, 18.70, 7.60, 5.20, 2.60),
('2025-04-10', 4, 4, 6.45, 14.50, 24.30, 10.10, 8.90, 3.50),
('2025-04-12', 5, 5, 7.10, 20.00, 30.00, 11.50, 12.00, 4.20),
('2025-04-15', 6, 6, 6.80, 18.60, 28.40, 9.90, 10.50, 4.00),
('2025-04-18', 7, 7, 5.50, 5.80, 12.60, 6.20, 3.70, 2.10),
('2025-04-20', 8, 8, 6.25, 11.90, 21.50, 8.80, 6.30, 3.20),
('2025-04-23', 9, 9, 7.00, 17.40, 25.60, 10.40, 9.70, 3.90),
('2025-04-25', 10, 10, 6.60, 13.20, 19.80, 7.90, 7.40, 2.70);