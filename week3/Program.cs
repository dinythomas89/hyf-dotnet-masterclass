var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/class-exer1", () =>
{
    var temperature = new Temperature(300);
    Console.WriteLine($"{temperature.Celsius} Celsius is {temperature.ConvertToFahrenheit()} Fahrenheit");
});

app.MapGet("/class-exer2", () =>
{
    var amount = 100;
    var exchangeRate = new ExchangeRate("EUR", "DKK");
    exchangeRate._rate = 7.5m;
    Console.WriteLine($"{amount} {exchangeRate.FromCurrency} is {exchangeRate.Calculate(amount)} {exchangeRate.ToCurrency}");

});

app.MapGet("/hw-exer1", () =>
{
    var account = new Account(100.56m);
    account.Deposit(100m);
    Console.WriteLine($"Account balance is {account.Balance}");
    account.Withdraw(20);
    Console.WriteLine($"Account balance is {account.Balance}");
    //account.Withdraw(200);
});

app.MapGet("/hw-exer2", () =>
{
    var myCow = new Cow();
    MakeSound(myCow);
    var myCat = new Cat();
    MakeSound(myCat);
    var myDog = new Dog();
    MakeSound(myDog);
});

void MakeSound(IAnimal animal)
{
    Console.WriteLine($"{animal.Name} says {animal.Sound}");
};

app.Run();

// class- exercise 1
public class Temperature
{
    public decimal Celsius { get; private set; }
    public Temperature(decimal celsius)
    {
        if (Celsius < 273.15m)
        {
            throw new Exception("Temperature is less than 273.15");
        }
        Celsius = celsius;
    }
    public decimal ConvertToFahrenheit()
    {
        return (Celsius * 9 / 5) + 32;
    }
}

//class exercise 2
public class ExchangeRate
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public decimal _rate{get;set;}
    public ExchangeRate(string fromCurrency, string toCurrency)
    {
        FromCurrency = fromCurrency;
        ToCurrency = toCurrency;
    }
    public decimal Calculate(decimal amount)
    {
        return _rate * amount;
    }
}

//Homework task1
public class Account
{
    public decimal Balance { get; set; }
    public Account(decimal balance)
    {
        Balance = balance;
    }

    public decimal Withdraw(decimal withdraw)
    {
        if (Balance > withdraw)
        {
            Balance -= withdraw;
            return Balance;
        }
        throw new Exception("You don't have this amount in your account to withdraw");


    }
    public decimal Deposit(decimal deposit)
    {
        Balance += deposit;
        return Balance;
    }

}

//Homework task2
public interface IAnimal
{
    string Name { get; }
    string Sound { get; }
}

public class Cow : IAnimal
{
    public string Name { get; } = "cowsy";
    public string Sound { get; } = "moow";
}

public class Cat : IAnimal
{
    public string Name { get; } = "pussy";
    public string Sound { get; } = "meow";
}

public class Dog : IAnimal
{
    public string Name { get; } = "tommy";
    public string Sound { get; } = "woff";
}

