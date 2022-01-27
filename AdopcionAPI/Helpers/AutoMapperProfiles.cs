using AdopcionAPI.DTOs;
using AdopcionAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CentroCreacionDTO, Centro>();
            CreateMap<Centro, CentroDTO>().ReverseMap();
            CreateMap<Centro, CentroConsultaDetalladaDTO>();
            CreateMap<Centro, CentroConsultaConMascotasDTO>();
            CreateMap<MascotaCreacionDTO, Mascota>();
            CreateMap<Mascota, MascotaDTO>().ReverseMap();
            CreateMap<Mascota, MascotaConsultaConCentroDTO>();
            CreateMap<Adopcion, AdopcionDTO>().ReverseMap();
            CreateMap<AdopcionCreacionDTO, Adopcion>();
            CreateMap<Adopcion,AdopcionConsultaConMascotaAdoptante>();
            
            CreateMap<Adoptante, AdoptanteSinAdopcionesDTO>();
            CreateMap<Adoptante, AdoptanteDTO>();
            CreateMap<AdoptanteCreacionDTO, Adoptante>();
        
        }   
    }
}
