var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// A POST endpoint that calls POST users/add with a record with FirstName, LastName and Age
app.MapPost("users/new", async (User addUser) => await CreateUser(addUser));

async Task<User?> CreateUser(User addUser)
{
    try
    {
        var response = await new HttpClient().PostAsJsonAsync("https://dummyjson.com/users/add", addUser);
        if (response == null)
        {
            return (User)Results.NotFound();
        }
        var newUser = await response.Content.ReadFromJsonAsync<User>();
        return newUser;
    }
    catch (Exception e)
    {
        throw new Exception(e.Message);
    }
};

// A POST endpoint that that calls Post products/add with a record with Title and Price
app.MapPost("products/new", async (Product addProduct) => await CreateProduct(addProduct));

async Task<Product?> CreateProduct(Product addProduct)
{
    try
    {
        var response = await new HttpClient().PostAsJsonAsync("https://dummyjson.com/products/add", addProduct);
        var newProduct = await response.Content.ReadFromJsonAsync<Product>();
        return newProduct;
    }
    catch (Exception e)
    {
        throw new Exception(e.Message);
    }
};

// Optional
//A GET endpoint that gets a user based on an id
app.MapGet("/user/{id}", async (int id) =>
{
    var response = await new HttpClient().GetAsync($"https://dummyjson.com/users/{id}");
    return response.Content.ReadFromJsonAsync<User>();
});

//A GET endpoint that gets a product based on an id
app.MapGet("/product/{id}", async (int id) =>
{
    var response = await new HttpClient().GetAsync($"https://dummyjson.com/product/{id}");
    return response.Content.ReadFromJsonAsync<Product>();
});

//A PUT endpoint that updates a user based on an id and the body of the request
app.MapPut("/user/{id}", async (int id, User updateUser) =>
{
    var response = await new HttpClient().PutAsJsonAsync($"https://dummyjson.com/users/{id}", updateUser);
    if (response == null)
        return Results.NotFound();
    var data = response.Content.ReadFromJsonAsync<User>();
    return Results.Ok(data);
});

//A PUT endpoint that updates a product based on an id and the body of the request
app.MapPut("/product/{id}", async (int id, Product updateProduct) =>
{
    var response = await new HttpClient().PutAsJsonAsync($"https://dummyjson.com/products/{id}", updateProduct);
    if (response == null)
        return Results.NotFound();
    var data = response.Content.ReadFromJsonAsync<Product>();
    return Results.Ok(data);
});

//A DELETE endpoint that deletes a user based on an id
app.MapDelete("/user/{id}", async (int id) =>
{
    var response = await new HttpClient().DeleteAsync($"https://dummyjson.com/users/{id}");
    if (response == null)
        return Results.NotFound();
    var data = response.Content.ReadFromJsonAsync<User>();
    return Results.Ok(data);
});

//A DELETE endpoint that deletes a product based on an id
app.MapDelete("/product/{id}", async (int id) =>
{
    var response = await new HttpClient().DeleteAsync($"https://dummyjson.com/products/{id}");
    if (response == null)
        return Results.NotFound();
    var data = response.Content.ReadFromJsonAsync<Product>();
    return Results.Ok(data);
});

app.Run();

record User(string FirstName, string LastName, int Age);
record Product(string Title, double Price);