/*
 * User API
 *
 * A simple API to manage users
 *
 * The version of the OpenAPI document: 1.0.0
 *
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using UsersApi.Attributes;
using UsersApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsersApi.Repositories;
using UsersApi.Data.Entites;

namespace UsersApi.Controllers;

/// <summary>
/// Users API controller
/// </summary>
[ApiController]
public class UserApiController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserApiController" /> class
    /// </summary>
    /// <param name="userRepository"></param>
    public UserApiController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <remarks>Creates a new user</remarks>
    /// <param name="createUserRequestDto">User to create</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="201">User created</response>
    [HttpPost]
    [Route("/user")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("CreateUser")]
    [SwaggerResponse(statusCode: 201, type: typeof(UserDto), description: "User created")]
    public virtual async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = createUserRequestDto.Name,
            Email = createUserRequestDto.Email,
            Phone = createUserRequestDto.Phone
        };

        var createdUser = await _userRepository.CreateUserAsync(user, cancellationToken);
        if (createdUser == null)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError);
        }

        var userDto = new UserDto(createdUser);
        return this.Created($"/user/{userDto.Id}", userDto);

    }

    /// <summary>
    /// Delete a user by id
    /// </summary>
    /// <remarks>Deletes a user by id</remarks>
    /// <param name="id">The user id</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="204">User deleted successfully</response>
    [HttpDelete]
    [Route("/user/{id}")]
    [ValidateModelState]
    [SwaggerOperation("DeleteUserById")]
    public virtual async Task<IActionResult> DeleteUserById([FromRoute(Name = "id")][Required] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _userRepository.DeleteUserByIdAsync(id, cancellationToken);
        if (!deleted)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }

    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <remarks>Returns a user by id</remarks>
    /// <param name="id">The user id</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">A user</response>
    [HttpGet]
    [Route("/user/{id}")]
    [ValidateModelState]
    [SwaggerOperation("GetUserById")]
    [SwaggerResponse(statusCode: 200, type: typeof(UserDto), description: "A user")]
    public virtual async Task<IActionResult> GetUserById([FromRoute(Name = "id")][Required] Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
        if (user == null)
        {
            return this.NotFound();
        }

        var userDto = new UserDto(user);
        return this.Ok(userDto);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <remarks>Returns all users</remarks>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">A list of users</response>
    [HttpGet]
    [Route("/users")]
    [ValidateModelState]
    [SwaggerOperation("GetUsers")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<UserDto>), description: "A list of users")]
    public virtual async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync(cancellationToken);
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            userDtos.Add(new UserDto(user));
        }

        return this.Ok(userDtos);
    }

    /// <summary>
    /// Update a user by id
    /// </summary>
    /// <remarks>Updates a user by id</remarks>
    /// <param name="id">The user id</param>
    /// <param name="userDto">User to update</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <response code="200">User updated</response>
    [HttpPut]
    [Route("/user/{id}")]
    [Consumes("application/json")]
    [ValidateModelState]
    [SwaggerOperation("UpdateUserById")]
    [SwaggerResponse(statusCode: 200, type: typeof(UserDto), description: "User updated")]
    public virtual async Task<IActionResult> UpdateUserById([FromRoute(Name = "id")][Required] Guid id, [FromBody] UserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = id,
            Name = userDto.Name,
            Email = userDto.Email,
            Phone = userDto.Phone
        };

        var updatedUser = await _userRepository.UpdateUserAsync(id, user, cancellationToken);
        if (updatedUser == null)
        {
            return this.NotFound();
        }

        var updatedUserDto = new UserDto(updatedUser);
        return this.Ok(updatedUserDto);
    }
}
