using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddPatientDto, Patient>();
            CreateMap<Patient, GetPatientDto>();
            CreateMap<UpdatePatientDto, Patient>();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Interaction, InteractionsDto>().ReverseMap();
            CreateMap<UserWithRoles,  UserWithRolesDto>();
            CreateMap<UpdateInteractionsDto, Interaction>();
        }
    }
}