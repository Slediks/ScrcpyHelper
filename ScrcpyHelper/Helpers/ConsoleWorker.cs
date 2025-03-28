namespace ScrcpyHelper.Helpers;

public static class ConsoleWorker
{
    public static void WriteToConsole(string[] text)
    {
        foreach (var s in text)
        {
            Console.WriteLine(s);
        }
    }

    public static int WriteListAndReadNumber(string header, string[] items)
    {
        Console.WriteLine(header);
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
        Console.WriteLine("Введите номер:");
        string input = Console.ReadLine() ?? string.Empty;
        while (!int.TryParse(input, out var result) || result <= 0 || result > items.Length)
        {
            input = Console.ReadLine() ?? string.Empty;
        }
        return int.Parse(input) - 1;
    }

    public static int WriteAndReadNumber(string header, int min, int max)
    {
        Console.WriteLine(header);
        string input = Console.ReadLine() ?? string.Empty;
        while (!int.TryParse(input, out var result) || result < min || result > max)
        {
            input = Console.ReadLine() ?? string.Empty;
        }
        
        return int.Parse(input);
    }
}