using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoYo_Web_App.Dtos;

namespace YoYo_Web_App.Helpers
{
    public class AthletMappingDto : Profile
    {
        //Need to get Entitie and DTO
        public AthletMappingDto()
        {
            CreateMap<Athlet, AthletDto>();
            CreateMap<FitnessScore, FitnessScoreDto>();

        }
    }
}
