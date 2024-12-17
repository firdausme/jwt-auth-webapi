using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.DTOs.Responses;

namespace JwtAuthWebApi.Services;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse?> GetByIdAsync(int id);
    Task<UserResponse?> GetByUsernameAsync(string username);
    Task CreateAsync(UserRequest request);
    Task UpdateAsync(UserRequest request);
    Task DeleteAsync(int id);
}