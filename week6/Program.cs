using HackYourFuture.Week6;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users", async (IUserRepository userRepository) =>
{
    return await userRepository.GetUsers();
});

app.MapPost("/users", async (IUserRepository userRepository, User user) =>
{
    return await userRepository.PostUser(user);
});

app.MapPut("/users/{id}", async (IUserRepository userRepository, User user, int id) =>
{
    return await userRepository.UpdateUser(user, id);
});

app.MapDelete("/users/{id}", async (IUserRepository userRepository, int id) =>
{
    return await userRepository.DeleteUser(id);
});

app.MapGet("/products", async (IProductRepository productRepository) =>
{
    return await productRepository.GetProducts();
});

app.MapPost("/products", async (IProductRepository productRepository, Product product) =>
{
    return await productRepository.PostProduct(product);
});

app.MapPut("/products/{id}", async (IProductRepository productRepository, Product product, int id) =>
{
    return await productRepository.UpdateProduct(product, id);
});

app.MapDelete("/products/{id}", async (IProductRepository productRepository, int id) =>
{
    return await productRepository.DeleteProduct(id);
});

app.Run();
