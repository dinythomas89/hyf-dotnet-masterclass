var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Homework
app.MapGet("/hw-task", (string str) =>
{
    string[] words = str.Split(' ');

    // method 1
    List<Tuple<string, int>> result = new List<Tuple<string, int>>();
    foreach (var item in words)
    {
        result.Add(new Tuple<string, int>(item, item.Length));
    }
    // Output: [{"item1":"The","item2":3},{"item1":"quick","item2":5},{"item1":"brown","item2":5},{"item1":"fox","item2":3},{"item1":"jumps","item2":5},{"item1":"over","item2":4},{"item1":"the","item2":3},{"item1":"lazy","item2":4},{"item1":"dog","item2":3}]

    // //method 2
    // Dictionary<string, int> result = new Dictionary<string, int>();
    // foreach (var item in words)
    // {
    //     result.Add(item, item.Length);
    // }
    // // Output: {"The":3,"quick":5,"brown":5,"fox":3,"jumps":5,"over":4,"the":3,"lazy":4,"dog":3}

    return result;
});

// Preparation exercises

app.MapGet("/prep-task1", () =>
{
    List<int> numbers = new List<int>() { 2, 5, 7, 8, 4 };
    List<int> evenNumbers = numbers.FindAll(n => n % 2 == 0);
    List<int> oddNumbers = numbers.FindAll(n => n % 2 != 0);
    return Results.Ok(new { EvenNumbers = evenNumbers, OddNumbers = oddNumbers });
});

app.MapGet("/prep-task2", (string str) => CharacterList(str));

List<char> CharacterList(string str)
{
    List<char> characters = new List<char>();
    foreach (char c in str)
        if (!Char.IsWhiteSpace(c))
        {
            characters.Add(c);
        }
    return characters;
};

app.MapGet("/prep-task3", (string str) =>
{
    List<char> characterList = CharacterList(str);
    char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
    int vowelCount = 0;
    foreach (char c in characterList)
    {
        if (vowels.Contains(c)) vowelCount++;
    }
    return vowelCount;
});

app.MapGet("/prep-task4", (string str) =>
{
    List<char> characterList = CharacterList(str);
    int firstOccurenceOfS = characterList.FindIndex(x => x == 's');
    int lastOccurenceOfE = characterList.FindLastIndex(x => x == 'e');
    return Results.Ok(new { FirstOccurenceOfS = firstOccurenceOfS, LastOccurenceOfE = lastOccurenceOfE }); ;
});

app.MapGet("/prep-task5", () => NumbersToHundredList());

List<int> NumbersToHundredList()
{
    List<int> numbers = new List<int>();
    for (int i = 0; i <= 100; i++)
    {
        numbers.Add(i);
    }
    return numbers;
};

app.MapGet("/prep-task6", () =>
{
    List<int> numbersToHundredList = NumbersToHundredList();
    numbersToHundredList.RemoveRange(33, 12);
    //numbersToHundredList.ForEach(Console.WriteLine);
    return numbersToHundredList;
});

app.MapGet("/prep-task7", () =>
{
    // I did only 1st part of the 7th exercise - to split the list.
    // But couldn't proceed to next as the return type here is in string.
    // I tried Chunk and GroupBY to split the list and its return is in string.
    List<int> numbersToHundredList = NumbersToHundredList();
    int numberOfElementsInSplittedList = (numbersToHundredList.Count + 1) / 2;

    string splittedList = System.Text.Json.JsonSerializer.Serialize(numbersToHundredList.Chunk(numberOfElementsInSplittedList));
    return splittedList;

    // var splittedList = numbersToHundredList.Select((x, idx) => new { index = idx, val = x })
    //         .GroupBy(i => i.index / numberOfElementsInSplittedList)
    //         .Select(g => g.Select(x => x.val));
    // return splittedList;
});

// Class exercises
app.MapGet("/class-task1", (string number1, string number2, string operation) =>
{
    bool numberOneIsValid = int.TryParse(number1, out int num1);
    bool numberTwoIsValid = int.TryParse(number2, out int num2);
    int result = 0;
    if (!numberOneIsValid && !numberTwoIsValid)
    {
        return Results.BadRequest("Not a valid number");
    }
    switch (operation)
    {
        case "add":
            result = num1 + num2;
            break;
        case "substract":
            result = num1 - num2;
            break;
        case "multiply":
            result = num1 * num2;
            break;
        default:
            return Results.BadRequest("Not a valid operation");
    }
    return Results.Ok(result);

});

app.MapGet("/class-task2", (string input) =>
{
    bool isNumber = int.TryParse(input, out var number);
    if (isNumber) return Results.Ok(AddNumbers(number));
    return Results.Ok(CountCapitalLetters(input));

    int AddNumbers(int input)
    {
        int[] digits = input.ToString().Select(digit => Convert.ToInt32(digit.ToString())).ToArray();
        //var digits = input.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
        int results = 0;
        foreach (int i in digits)
        {
            results += i;
        }
        return results;
    };

    int CountCapitalLetters(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (char.IsUpper(c))
                count++;
        }
        return count;
    }
});

app.MapGet("/class-task3", (string input) =>
{
    var result = new List<char>();
    char[] characters = input.ToLower().ToCharArray();
    foreach (char c in characters)
    {
        if (!Char.IsWhiteSpace(c))
        {
            result.Add(c);
        }
        result.Sort();
    }
    return result.Distinct().ToList(); ;
});

app.Run();