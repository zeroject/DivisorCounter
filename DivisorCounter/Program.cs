// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Diagnostics;
using Dapper;
using MySqlConnector;

public class Program
{
    private static IDbConnection divisorCache = new MySqlConnection("Server=cache-db;Database=cache-database;Uid=div-cache;Pwd=C@ch3d1v;");
    
    public static void Main()
    {
        var first = 1_000_000_000;
        var last = 1_000_000_020;

        var numberWithMostDivisors = first;
        var result = 0;

        divisorCache.Open();
        var tables = divisorCache.Query<string>("SHOW TABLES LIKE 'counters'");
        if (!tables.Any())
        {
            divisorCache.Execute("CREATE TABLE counters (number BIGINT NOT NULL PRIMARY KEY, divisors INT NOT NULL)");
        }
        
        var watch = Stopwatch.StartNew();
        for (var i = first; i <= last; i++)
        {
            var innerWatch = Stopwatch.StartNew();
            var divisorCounter = divisorCache.QueryFirstOrDefault<int>("SELECT divisors FROM counters WHERE number = @number", new { number = i });

            if (divisorCounter == 0)
            {
                for (var divisor = 1; divisor <= i; divisor++)
                {
                    if (i % divisor == 0)
                    {
                        divisorCounter++;
                    }
                }

                divisorCache.Execute("INSERT INTO counters (number, divisors) VALUES (@number, @divisors)", new { number = i, divisors = divisorCounter });
            }
            
            innerWatch.Stop();
            Console.WriteLine("Counted " + divisorCounter + " divisors for " + i + " in " + innerWatch.ElapsedMilliseconds + "ms");

            if (divisorCounter > result)
            {
                numberWithMostDivisors = i;
                result = divisorCounter;
            }
        }
        watch.Stop();
        
        Console.WriteLine("The number with most divisors inside range is: " + numberWithMostDivisors + " with " + result + " divisors.");
        Console.WriteLine("Elapsed time: " + watch.ElapsedMilliseconds + "ms");
        Console.ReadLine();
    }
}