using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> PostUser(User user);
    Task<User> UpdateUser(User user, int id);
    Task<User> DeleteUser(int id);
}

public class UserRepository : IUserRepository
{
    private string connectionString;

    public UserRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        using var connection = new MySqlConnection(connectionString);
        var users = await connection.QueryAsync<User>("SELECT * FROM dotnetweek6.user");
        return users;
    }

    public async Task<User> PostUser(User user)
    {
        await using var connection = new MySqlConnection(connectionString);
        var newUser = await connection.ExecuteAsync("INSERT INTO dotnetweek6.user (name, email) VALUES (@name, @email)", user);
        return user;
    }

    public async Task<User> UpdateUser(User user, int id)
    {
        await using var connection = new MySqlConnection(connectionString);
        var updatedUser = await connection.ExecuteAsync($"UPDATE user SET name=@name, email=@email WHERE id={id}", user);
        return user;
    }

    public async Task<User> DeleteUser(int id)
    {
        await using var connection = new MySqlConnection(connectionString);
        var deletedUser = await connection.ExecuteAsync($"DELETE FROM user WHERE id={id}", new { ID = id });
        return (User)Results.Ok();
    }
}

public record User(int id, string name, string email);