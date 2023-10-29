using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    [HttpPost] 
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)//the ActionResult is an HTTP response type, which contains various extra data, other than what we provide
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.UserName}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{username:string}")] //we mark the method with [HttpGet] so that GET requests to this controller end here
    //the return value is the IEnumerable<User> wrapped in an HTTP response message
    public async Task<ActionResult<IEnumerable<User>>> GetUserAsync([FromRoute] string username)//the argument is marked as [FromQuery] to indicate that this argument should be extracted from the query parameters of the URI
    {
        try
        {
            UserBasicDto userBasicDto = await userLogic.GetUser(username);
            return Ok(userBasicDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}