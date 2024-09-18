using AutoMapper;
using MotoDev.Common.Dtos;
using MotoDev.Domain.Entities;

namespace MotoDev.Application.Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRequest>()
                .ForMember(x => x.RepairShopUserId, s => s.Ignore())
                .ForMember(x => x.Password, s => s.Ignore())
                .ReverseMap();
            CreateMap<UserRequest, User>()
                .ForMember(x => x.ResetPasswordToken, s => s.Ignore())
                .ForMember(x => x.Password, s => s.Ignore())
                .ForMember(x => x.Id, s => s.Ignore())
                .ForMember(x => x.LastUpdatedAt, s => s.Ignore())
                .ForMember(x => x.LastUpdatedByUserId, s => s.Ignore())
                .ForMember(x => x.IsActive, s => s.Ignore())
                .ReverseMap();

            CreateMap<User, UserExtendedResponse>();
            CreateMap<UserExtendedResponse, User>();

            CreateMap<RepairShopUser, RepairShopUserResponse>();
            CreateMap<RepairShopUserResponse, RepairShopUserResponse>();

            CreateMap<RepairShop, RepairShopResponse>();
            CreateMap<RepairShopResponse, RepairShop>();

            CreateMap<RepairShopRequest, RepairShopResponse>();
            CreateMap<RepairShopResponse, RepairShopRequest>();

            CreateMap<RepairShop, RepairShopRequest>();
            CreateMap<RepairShopRequest, RepairShop>();


        }
    }
}