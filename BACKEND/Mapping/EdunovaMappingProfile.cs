using AutoMapper;
using BACKEND.DTOs;
using BACKEND.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BACKEND.Mapping
{
    /// <summary>
    /// AutoMapper profil koji definira mapiranja izmeðu entiteta i DTO objekata u backendu.
    /// </summary>
    public class BackendMappingProfile : Profile
    {
        /// <summary>
        /// Konstruktor koji postavlja sva mapiranja izmeðu entiteta i DTO-eva.
        /// </summary>
        public BackendMappingProfile()
        {
            // Analiza -> AnalizaDto
            CreateMap<Analiza, AnalizaDto>()
                .ForMember(dest => dest.MasaUzorka, opt => opt.MapFrom(src => src.UzorakTla.MasaUzorka))
                .ForMember(dest => dest.VrstaTla, opt => opt.MapFrom(src => src.UzorakTla.VrstaTla))
                .ForMember(dest => dest.DatumUzorka, opt => opt.MapFrom(src => src.UzorakTla.Datum))
                .ForMember(dest => dest.MjestoUzorkovanja, opt => opt.MapFrom(src => src.UzorakTla.Lokacija.MjestoUzorkovanja))
                .ForMember(dest => dest.Ime, opt => opt.MapFrom(src => src.Analiticar.Ime))
                .ForMember(dest => dest.Prezime, opt => opt.MapFrom(src => src.Analiticar.Prezime))
                .ForMember(dest => dest.Kontakt, opt => opt.MapFrom(src => src.Analiticar.Kontakt))
                .ForMember(dest => dest.StrucnaSprema, opt => opt.MapFrom(src => src.Analiticar.StrucnaSprema));

            // AnalizaCreateUpdateDto -> Analiza
            CreateMap<AnalizaCreateUpdateDto, Analiza>()
            .ForMember(dest => dest.UzorakTla, opt => opt.MapFrom(src => new Uzorcitla
            {
                MasaUzorka = src.MasaUzorka,
                VrstaTla = src.VrstaTla ?? "",
                Datum = src.DatumUzorka ?? DateTime.Now,
                Lokacija = new Lokacija { MjestoUzorkovanja = src.MjestoUzorkovanja ?? "" }
            }))
            .ForMember(dest => dest.Analiticar, opt => opt.MapFrom(src => new Analiticar
            {
                Ime = src.Ime ?? "",
                Prezime = src.Prezime ?? "",
                Kontakt = src.Kontakt ?? "",
                StrucnaSprema = src.StrucnaSprema ?? ""
            }));


            // Uzorcitla -> UzorcitlaDto
            CreateMap<Uzorcitla, UzorcitlaDto>()
                .ForMember(dest => dest.MjestoUzorkovanja, opt => opt.MapFrom(src => src.Lokacija.MjestoUzorkovanja));

            // UzorcitlaDto -> Uzorcitla
            CreateMap<UzorcitlaDto, Uzorcitla>();

            // Analiticar -> DTO (ako æeš raditi posebne DTO-e za analitièare)
            CreateMap<Analiticar, AnalizaCreateUpdateDto>();
        }
    }
}
