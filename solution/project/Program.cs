using System.Numerics;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Render задаёт порт через переменную окружения
        var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
        builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

        var app = builder.Build();

        app.MapGet("/vugorenko_gmail_com", (HttpRequest req) =>
        {
            var query = req.Query;

            if (!query.TryGetValue("x", out var queryValueX) ||
                !query.TryGetValue("y", out var queryValueY))
            {
                return Results.Text("NaN", "text/plain");
            }

            string stringX = queryValueX.ToString();
            string stringY = queryValueY.ToString();

            // Проверяем, что оба значения содержат только цифры
            if (!Regex.IsMatch(stringX, @"^[0-9]+$") || !Regex.IsMatch(stringY, @"^[0-9]+$"))
            {
                return Results.Text("NaN", "text/plain");
            }

            if (!BigInteger.TryParse(stringX, out BigInteger numberX) ||
                !BigInteger.TryParse(stringY, out BigInteger numberY))
            {
                return Results.Text("NaN", "text/plain");
            }

            if (numberX <= 0 || numberY <= 0)
            {
                return Results.Text("NaN", "text/plain");
            }

            BigInteger gcd = CalculateGcd(numberX, numberY);
            BigInteger lcm = (numberX / gcd) * numberY;

            return Results.Text(lcm.ToString(), "text/plain");
        });

        app.Run();

        static BigInteger CalculateGcd(BigInteger first, BigInteger second)
        {
            first = BigInteger.Abs(first);
            second = BigInteger.Abs(second);

            while (second != 0)
            {
                BigInteger temporary = first % second;
                first = second;
                second = temporary;
            }

            return first;
        }
    }
}
