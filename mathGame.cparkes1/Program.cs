using System.Data;
using System.Numerics;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

Random random = new Random();
List<GameResult> scoreHistory = new();

string userSelection = "";

while (true)
{
    int score = 0;
    int turnCounter = 0;

    Console.Clear();
    Console.WriteLine("""
        What Mathematical operator do you want?
        +       Answer 5 Addition Questions
        -       Answer 5 Subtraction Questions
        *       Answer 5 Multiplication Questions
        /       Answer 5 Division Questions
        Scores  Show the previous Scores
        Exit    Exit the Program
        """);

    userSelection = Console.ReadLine() ?? "";

    if (userSelection.ToLower() == "exit")
    {
        break;
    }
    else if (userSelection.ToLower() == "scores")
    {
        Console.Clear();
        if (scoreHistory.Count == 0)
        {
            Console.WriteLine("No scores yet.");
            Console.WriteLine("Press enter to return to main menu");
            Console.ReadLine();
        }

        List<GameResult> sortedLeaderboard = scoreHistory
            .OrderByDescending(r => r.Score)
            .ThenBy(r => r.Time)
            .ToList();

        Console.WriteLine("Previous Scores:");
        foreach (var s in sortedLeaderboard)
            Console.WriteLine(s);
        Console.WriteLine("Press enter to return to main menu");
        Console.ReadLine();
        continue;
    }
    else if (userSelection != "+" && userSelection != "-" && userSelection != "*" && userSelection != "/")
    {
        Console.Clear();
        Console.WriteLine("Invalid Option. Please try again.");
        Console.WriteLine("Press enter to return to main menu");
        Console.ReadLine();
    }

    Console.Clear();
    long startTime = Stopwatch.GetTimestamp();
    do
    {
        int firstNumber = random.Next(101);
        int secondNumber = random.Next(101);
        int expectedSolution = 0;

        if (userSelection.Equals("/") && firstNumber == 0 || userSelection.Equals("/") && secondNumber == 0 || userSelection.Equals("/") && firstNumber % secondNumber != 0)
        {
            firstNumber = random.Next(101);
            secondNumber = random.Next(101);
            continue;
        }

        Console.Write($"{firstNumber} {userSelection} {secondNumber} = ");
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int userSolution))
        {
            Console.WriteLine("\nInvalid input. Please enter an integer.");
            continue;
        }

        switch (userSelection)
        {
            case "+": expectedSolution = firstNumber + secondNumber; break;
            case "-": expectedSolution = firstNumber - secondNumber; break;
            case "*": expectedSolution = firstNumber * secondNumber; break;
            case "/": expectedSolution = firstNumber / secondNumber; break;
        }
        ;

        if (userSolution == expectedSolution)
        {
            Console.WriteLine("\n Correct\n");
            score++;
        }
        else
        {
            Console.WriteLine("\nIncorrect\n");
        }

        turnCounter++;
    } while (turnCounter < 5);

    TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTime);
    string formattedElapsedTime = elapsedTime.ToString(@"mm\:ss\.fff");
    Console.WriteLine($"Congrats you got {score} points in {formattedElapsedTime}");
    scoreHistory.Add(new GameResult(score, elapsedTime));
    Console.WriteLine("Press enter to return to main menu");
    Console.ReadLine();
}

