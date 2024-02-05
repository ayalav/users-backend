namespace UsersApi.Repositories;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersApi.Data;
using UsersApi.Data.Entites;

/// <summary>
/// Represents a repository for managing users
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created user.</returns>
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> UpdateUserAsync(Guid id, User user, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
}


/// <summary>
/// Implements a repository for managing users
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Creates a new instance of <see cref="UserRepository"/>
    /// </summary>
    /// <param name="context"></param>
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        if (user.Id == Guid.Empty)
        {
            user.Id = Guid.NewGuid();
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Gets a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = new object[] { id };
        return await _context.Users.FindAsync(filter, cancellationToken);
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<User> UpdateUserAsync(Guid id, User user, CancellationToken cancellationToken)
    {
        var existingUser = await GetUserByIdAsync(id, cancellationToken);
        if (existingUser == null)
        {
            return null;
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Phone = user.Phone;
        await _context.SaveChangesAsync(cancellationToken);
        return existingUser;
    }

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
