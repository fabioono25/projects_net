using System;
using AutoMapper;
using SimpleAPI.Dtos;
using SimpleAPI.Models;

namespace SimpleAPI.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<Person, PersonReadDto>();
        }   
    }
}

