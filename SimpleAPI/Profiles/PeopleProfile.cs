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
            // source -> target
            CreateMap<Person, PersonReadDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, PersonUpdateDto>();
        }   
    }
}

