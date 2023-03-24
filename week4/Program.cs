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

// A POST endpoint that takes a lists of ids and retrieves all of the users with those ids from the GET users (Id, FirstName, LastName and Age)
app.MapPost("/userIds", async (List<int> userIds) => await GetUsersAsync(userIds));

async Task<List<User>> GetUsersAsync(List<int> userIds)
{
    var getUserTasks = new List<Task<User>>();
    foreach (int userId in userIds)
    {
        getUserTasks.Add(GetUserAsync(userId));
    }
    var users = await Task.WhenAll(getUserTasks);
    return new List<User>(users);
}

async Task<User> GetUserAsync(int userId)
{
    var response = await new HttpClient().GetAsync($"https://dummyjson.com/users/{userId}");
    var data = await response.Content.ReadFromJsonAsync<User>();
    return data;
}

// A POST endpoint that takes a lists of ids and retrieves all of the products with those ids GET products(Id, Title)
app.MapPost("/productIds", async (List<int> productIds) => await GetProductsAsync(productIds));

async Task<List<Product>> GetProductsAsync(List<int> productIds)
{
    var getProductsTasks = new List<Task<Product>>();
    foreach (int productId in productIds)
    {
        getProductsTasks.Add(GetProductAsync(productId));
    }
    var products = await Task.WhenAll(getProductsTasks);
    return new List<Product>(products);
}

async Task<Product> GetProductAsync(int productId)
{
    var response = await new HttpClient().GetAsync($"https://dummyjson.com/product/{productId}");
    var data = await response.Content.ReadFromJsonAsync<Product>();
    return data;
};

// Optional
//A GET endpoint that gets a user based on an id
app.MapGet("/user/{id}", async (int userId) => await GetUserAsync(userId));

//A GET endpoint that gets a product based on an id
app.MapGet("/product/{id}", async (int productId) => await GetProductAsync(productId));

//A PUT endpoint that updates a user based on an id and the body of the request
app.MapPut("/user/{id}", async (int id, User updateUser) =>
{
    var response = await new HttpClient().PutAsJsonAsync($"https://dummyjson.com/users/{id}", updateUser);
    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Something went wrong");
    }
    var data = response.Content.ReadFromJsonAsync<User>();
    return Results.Ok(data);
});

//A PUT endpoint that updates a product based on an id and the body of the request
app.MapPut("/product/{id}", async (int id, Product updateProduct) =>
{
    var response = await new HttpClient().PutAsJsonAsync($"https://dummyjson.com/products/{id}", updateProduct);
    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Something went wrong");
    }
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