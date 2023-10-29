using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao: IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User> LoginAsync(User user)
    {
        user.Status = "active";
        User? existing = context.Users.FirstOrDefault(u => u.Equals(user));
        if (existing != null && existing.Status.Equals("unactive"))
        {
            context.Users.Remove(existing);
            context.Users.Add(user);
        }
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User> LogoutAsync(User user)
    {
        user.Status = "unactive";
        User? existing = context.Users.FirstOrDefault(u => u.Equals(user));
        if (existing != null && existing.Status.Equals("active"))
        {
            context.Users.Remove(existing);
            context.Users.Add(user);
        }
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User?> GetUserAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsernameAsync(string? userName)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }
}