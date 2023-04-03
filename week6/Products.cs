using Dapper;
using MySql.Data.MySqlClient;

namespace HackYourFuture.Week6;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> PostProduct(Product product);
    Task<Product> UpdateProduct(Product product, int id);
    Task<Product> DeleteProduct(int id);
}

public class ProductRepository : IProductRepository
{
    private string connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        using var connection = new MySqlConnection(connectionString);
        var products = await connection.QueryAsync<Product>("SELECT * FROM dotnetweek6.products");
        return (IEnumerable<Product>)products;
    }

    public async Task<Product> PostProduct(Product product)
    {
        await using var connection = new MySqlConnection(connectionString);
        var newProduct = await connection.ExecuteAsync("INSERT INTO dotnetweek6.products (name, price, description) VALUES (@name, @price, @description)", product);
        return product;
    }

    public async Task<Product> UpdateProduct(Product product, int id)
    {
        await using var connection = new MySqlConnection(connectionString);
        var updatedUser = await connection.ExecuteAsync($"UPDATE products SET name=@name, price=@price, description=@description WHERE id={id}", product);
        return product;
    }

    public async Task<Product> DeleteProduct(int id)
    {
        await using var connection = new MySqlConnection(connectionString);
        var deletedUser = await connection.ExecuteAsync($"DELETE FROM products WHERE id={id}");
        return (Product)Results.Ok();
    }
}


public record Product(int Id, string name, string price, string description);