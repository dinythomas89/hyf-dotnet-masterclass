var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/task1", (string str) =>
{
    string reversed = "";

    foreach (char c in str)
    {
        reversed = c + reversed;

    }
    return reversed;
});

app.MapGet("/task2", (string str) =>
{
    char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
    int vowelCount = 0;
    foreach (char c in str.ToLower())
    {
        if (vowels.Contains(c)) vowelCount++;
    }
    return $"Vowel count - {vowelCount}";
});

app.MapGet("/task3", (int[] numbers) =>
{
    int negSumResult = 0;
    int posMulResult = 1;
    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] > 0)
        {
            posMulResult *= numbers[i];
        }
        else
            negSumResult += numbers[i];
    }
    return $"Sum of negative numbers - {negSumResult} \nMultiplication of positive numbers - {posMulResult}";
});

app.MapGet("/task4", (int number) =>
{
    int firstNum = 0, secondNum = 1, nextNum = 0;
    for (int i = 2; i <= number; i++)
    {
        nextNum = firstNum + secondNum;
        firstNum = secondNum;
        secondNum = nextNum;
    }
    return $"Nth fibonacci number is {secondNum}";
});

app.MapGet("/task5", (int[] inputArr) =>
{
    string warningMsg = "The length of the array is an odd number";
    if (inputArr.Length % 2 != 0)
        Console.WriteLine(warningMsg);

    // int[] firstArr = inputArr.Take(2).ToArray();
    // int[] secondArr = inputArr.Skip(2).ToArray();

    int midLength = inputArr.Length / 2;
    int[] firstArr = new int[midLength];
    int[] secondArr = new int[midLength];
    Array.Copy(inputArr, firstArr, midLength);
    Array.Copy(inputArr, midLength, secondArr, 0, midLength);
    // Console.WriteLine(String.Join(", ", firstArr));
    int[] finalArr = new int[midLength];
    for (int i = 0; i < midLength; i++)
    {
        finalArr[i] = firstArr[i] + secondArr[i];

    }
    return finalArr;
});

app.Run();
