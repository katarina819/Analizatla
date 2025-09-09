using AutoMapper;
using BACKEND.DTOs;
using BACKEND.Models;
using BACKEND.Models.DTO;

namespace BACKEND.Mapping
{
    /// <summary>
    /// AutoMapper profil koji definira mapiranja izmeðu entiteta i DTO objekata u backendu.
    /// </summary>
    public class BackendMappingProfile : Profile
    {
        public BackendMappingProfile()
        {
            // Analiza -> AnalizaDto (read)
            CreateMap<Analiza, AnalizaDto>()
                .ForMember(dest => dest.MasaUzorka, opt => opt.MapFrom(src => src.UzorakTla.MasaUzorka))
                .ForMember(dest => dest.VrstaTla, opt => opt.MapFrom(src => src.UzorakTla.VrstaTla))
                .ForMember(dest => dest.DatumUzorka, opt => opt.MapFrom(src => src.UzorakTla.Datum))
                .ForMember(dest => dest.MjestoUzorkovanja,
                    opt => opt.MapFrom(src => src.UzorakTla != null && src.UzorakTla.Lokacija != null
                        ? src.UzorakTla.Lokacija.MjestoUzorkovanja
                        : ""))
                .ForMember(dest => dest.Ime,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.Ime : ""))
                .ForMember(dest => dest.Prezime,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.Prezime : ""))
                .ForMember(dest => dest.Kontakt,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.Kontakt : ""))
                .ForMember(dest => dest.StrucnaSprema,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.StrucnaSprema : ""));

            // Analiza -> AnalizaCreateUpdateDto (read za edit/view)
            CreateMap<Analiza, AnalizaCreateUpdateDto>()
                .ForMember(dest => dest.MasaUzorka, opt => opt.MapFrom(src => src.UzorakTla.MasaUzorka))
                .ForMember(dest => dest.VrstaTla, opt => opt.MapFrom(src => src.UzorakTla.VrstaTla))
                .ForMember(dest => dest.DatumUzorka, opt => opt.MapFrom(src => src.UzorakTla.Datum))
                .ForMember(dest => dest.MjestoUzorkovanja,
                    opt => opt.MapFrom(src => src.UzorakTla != null && src.UzorakTla.Lokacija != null
                        ? src.UzorakTla.Lokacija.MjestoUzorkovanja
                        : ""))
                .ForMember(dest => dest.Ime,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.Ime : ""))
                .ForMember(dest => dest.Prezime,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.Prezime : ""))
                .ForMember(dest => dest.Kontakt,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.Kontakt : ""))
                .ForMember(dest => dest.StrucnaSprema,
                    opt => opt.MapFrom(src => src.Analiticar != null ? src.Analiticar.StrucnaSprema : ""))
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src => src.Datum))
                .ForMember(dest => dest.pHVrijednost, opt => opt.MapFrom(src => src.pHVrijednost))
                .ForMember(dest => dest.Fosfor, opt => opt.MapFrom(src => src.Fosfor))
                .ForMember(dest => dest.Kalij, opt => opt.MapFrom(src => src.Kalij))
                .ForMember(dest => dest.Magnezij, opt => opt.MapFrom(src => src.Magnezij))
                .ForMember(dest => dest.Karbonati, opt => opt.MapFrom(src => src.Karbonati))
                .ForMember(dest => dest.Humus, opt => opt.MapFrom(src => src.Humus));

            // AnalizaCreateUpdateDto -> Analiza (create/update)
            CreateMap<AnalizaCreateUpdateDto, Analiza>()
                .ForMember(dest => dest.Datum, opt => opt.MapFrom(src =>
                    src.Datum.HasValue
                        ? DateTime.SpecifyKind(src.Datum.Value, DateTimeKind.Utc)
                        : DateTime.UtcNow))
                .ForMember(dest => dest.UzorakTla, opt => opt.MapFrom(src => new Uzorcitla
                {
                    MasaUzorka = src.MasaUzorka,
                    VrstaTla = src.VrstaTla ?? "",
                    Datum = src.DatumUzorka.HasValue
                                ? DateTime.SpecifyKind(src.DatumUzorka.Value, DateTimeKind.Utc)
                                : DateTime.UtcNow,
                    Lokacija = new Lokacija { MjestoUzorkovanja = src.MjestoUzorkovanja ?? "" }
                }))
                .ForMember(dest => dest.Analiticar, opt => opt.MapFrom(src => new Analiticar
                {
                    Ime = src.Ime ?? "",
                    Prezime = src.Prezime ?? "",
                    Kontakt = src.Kontakt ?? "",
                    StrucnaSprema = src.StrucnaSprema ?? ""
                }))
                .ForMember(dest => dest.pHVrijednost, opt => opt.MapFrom(src => src.pHVrijednost))
                .ForMember(dest => dest.Fosfor, opt => opt.MapFrom(src => src.Fosfor))
                .ForMember(dest => dest.Kalij, opt => opt.MapFrom(src => src.Kalij))
                .ForMember(dest => dest.Magnezij, opt => opt.MapFrom(src => src.Magnezij))
                .ForMember(dest => dest.Karbonati, opt => opt.MapFrom(src => src.Karbonati))
                .ForMember(dest => dest.Humus, opt => opt.MapFrom(src => src.Humus))
                .ForMember(dest => dest.Sifra, opt => opt.Ignore());

            // Uzorcitla -> UzorcitlaDto
            CreateMap<Uzorcitla, UzorcitlaDto>()
                .ForMember(dest => dest.MjestoUzorkovanja,
                    opt => opt.MapFrom(src => src.Lokacija != null ? src.Lokacija.MjestoUzorkovanja : ""));

            // UzorcitlaDto -> Uzorcitla (update)
            CreateMap<UzorcitlaDto, Uzorcitla>()
                .ForMember(dest => dest.Sifra, opt => opt.Ignore())
                .ForMember(dest => dest.Lokacija, opt => opt.Ignore());

            // Analiticar -> AnaliticarDTO (read)
            CreateMap<Analiticar, AnaliticarDTO>();

            // AnaliticarDTO -> Analiticar (create/update)
            CreateMap<AnaliticarDTO, Analiticar>()
                .ForMember(dest => dest.Sifra, opt => opt.Ignore())
                .ForMember(dest => dest.SlikaUrl, opt => opt.Ignore());
        }
    }
}
