using AutoMapper;
using FluentValidation;
using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.Exceptions;
using JwtAuthWebApi.Extensions;
using JwtAuthWebApi.Models;
using JwtAuthWebApi.Repositories;

namespace JwtAuthWebApi.Services;

public class UserService(
    IUserRepository repository,
    IMapper mapper,
    IValidator<UserRequest> validator
) : IUserService
{
    public async Task<List<UserResponse>> GetAllAsync()
    {
        return mapper.Map<List<UserResponse>>(
            await repository.GetAllAsync()
        );
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is null) throw new ApiException("User not found", StatusCodes.Status404NotFound);

        return mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse?> GetByUsernameAsync(string username)
    {
        var user = await repository.GetByUsernameAsync(username);
        if (user is null) throw new ApiException("User not found", StatusCodes.Status404NotFound);

        return mapper.Map<UserResponse>(user);
    }

    public async Task CreateAsync(UserRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var user = await repository.GetByUsernameAsync(request.Username);
        if (user is not null) throw new ApiException("User already exists", StatusCodes.Status409Conflict);

        await repository.CreateAsync(
            mapper.Map<User>(request)
        );
    }

    public async Task UpdateAsync(UserRequest request)
    {
        
        var user = await repository.GetByUsernameAsync(request.Username);
        if(user is null) throw new ApiException("User not found", StatusCodes.Status404NotFound);
        
        user.Password = request.Password;
        user.UserDetail.FullName = request.Fullname;
        user.UserDetail.DateOfBirth = request.DateOfBirth;
        
        await repository.UpdateAsync(
            mapper.Map<User>(user.EncryptPassword())
        );
    }

    public async Task DeleteAsync(int id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is null) throw new ApiException("User not found", StatusCodes.Status404NotFound);

        await repository.DeleteAsync(user);
    }
}