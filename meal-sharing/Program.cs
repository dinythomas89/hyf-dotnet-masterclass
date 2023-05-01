using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IMealService, FileMealService>();
var app = builder.Build();

app.MapGet("/", () => " World!");

app.MapGet("/meals", ([FromServices] IMealService mealService) => mealService.ListMeals());
app.MapPost("/meals/new", ([FromServices] IMealService mealService, Meal meal) => mealService.AddMeal(meal));

app.Run();

public class Meal
{
    public string Headline { get; set; }
    public string Image { get; set; }
    public string Body { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public Meal(string headline, string image, string body, string location, decimal price)
    {
        Headline = headline;
        Image = image;
        Body = body;
        Location = location;
        Price = price;
    }
}

public class FileMealService : IMealService
{
    public List<Meal> ListMeals()
    {
        var json = File.ReadAllText("meals.json");
        var mealsFromJson = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(json);
        return mealsFromJson;
    }

    public void AddMeal(Meal meal)
    {
        if (!File.Exists("meals.json"))
        {
            File.WriteAllText("meals.json", "[]");
        }
        var meals = System.Text.Json.JsonSerializer.Deserialize<List<Meal>>(File.ReadAllText("meals.json"));
        meals.Add(meal);
        string json = System.Text.Json.JsonSerializer.Serialize(meals);
        File.WriteAllText("meals.json", json);
    }
}

interface IMealService
{
    List<Meal> ListMeals();
    void AddMeal(Meal meal);
}
