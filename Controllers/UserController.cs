using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthWebApi.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class UserController(IUserService service) : Controller
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(
            ApiResponse<IEnumerable<UserResponse>>.Success(
                await service.GetAllAsync(),
                "Users Retrieved Successfully"
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        return Ok(
            ApiResponse<UserResponse>.Success(
                await service.GetByIdAsync(id),
                "User Retrieved Successfully"
            )
        );
    }

    [HttpGet("username")]
    public async Task<ActionResult> GetByUsername(string username)
    {
        return Ok(
            ApiResponse<UserResponse>.Success(
                await service.GetByUsernameAsync(username),
                "User Retrieved Successfully")
        );
    }

    [HttpPost]
    public async Task<ActionResult> Create(UserRequest request)
    {
        await service.CreateAsync(request);
        return CreatedAtAction(nameof(GetByUsername), new { username = request.Username }, request);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UserRequest request)
    {
        await service.UpdateAsync(request);
        return Ok(ApiResponse<object>.Success("User Updated Successfully"));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return Ok(ApiResponse<object>.Success("User Deleted Successfully"));
    }
}