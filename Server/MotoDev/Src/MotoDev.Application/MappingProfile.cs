using AutoMapper;
using MotoDev.Common.Dtos;
using MotoDev.Domain.Entities;

namespace MotoDev.Application.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();
            CreateMap<User, UserRequest>();
            CreateMap<UserRequest, User>();
        }
    }
}