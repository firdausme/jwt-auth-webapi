using AutoMapper;
using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.Models;

namespace JwtAuthWebApi.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.UserDetail.FullName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.UserDetail.DateOfBirth));

        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.UserDetail, opt => opt.MapFrom(src => new UserDetail()
            {
                FullName = src.Fullname,
                DateOfBirth = src.DateOfBirth
            }));
    }
}