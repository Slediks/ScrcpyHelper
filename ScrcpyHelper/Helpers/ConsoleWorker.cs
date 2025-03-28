using ScrcpyHelper.Objects;

namespace ScrcpyHelper.Helpers;

public static class ConsoleWorker
{
    public static void WriteToConsole(string text, bool clear = true)
    {
        if (clear) Console.Clear();
        Console.WriteLine(text);
    }

    public static int WriteListAndReadNumber(string header, string[] items, bool clear = true)
    {
        if (clear) Console.Clear();
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

    public static Tuple<int, string> WriteListAndRead2Args(string[] items, bool clear = true)
    {
        if (clear) Console.Clear();
        Console.WriteLine($"0. <-- Назад");
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"{i + 1}. | {items[i]}");
        }
        Console.WriteLine("Введите номер параметра и желаемую операцию или 0, чтобы вернуться назад");
        Console.WriteLine("Пример: 1 Change");
        var input = (Console.ReadLine() ?? string.Empty).Split(' ');
        // pars in0 && ((cnt 1 && result == 0) || (cnt 2 && result > 0 && result <= length && (in1 == Open || in1 == Change))
        // !pars in0 || ((!cnt 1 || result != 0) && (!cnt 2 || result <= 0 || result > length || (in1 != Open && in1 != Change))
        while (!int.TryParse(input[0], out var result) || 
               ((input.Length != 1 || result != 0) && 
                (input.Length != 2 || result <= 0 || result > items.Length || 
                 !items[result - 1].Contains($"{Properties.Brackets[0]}{input[1]}{Properties.Brackets[1]}"))))
        {
            input = (Console.ReadLine() ?? string.Empty).Split(' ');
        }
        
        var resInt = int.Parse(input[0]);
        return new Tuple<int, string>(resInt, resInt == 0 ? "" : input[1]);
    }

    public static int WriteAndReadNumber(string header, int min, int max, bool clear = true)
    {
        if (clear) Console.Clear();
        Console.WriteLine(header);
        string input = Console.ReadLine() ?? string.Empty;
        while (!int.TryParse(input, out var result) || result < min || result > max)
        {
            input = Console.ReadLine() ?? string.Empty;
        }
        
        return int.Parse(input);
    }
    
    public static string WriteAndReadString(string header, string[]? values = null, bool clear = true)
    {
        if (clear) Console.Clear();
        Console.WriteLine(header);
        if (values != null)
        {
            Console.WriteLine(string.Join('/', values));
        }
            
        string input = Console.ReadLine() ?? string.Empty;
        while (input == string.Empty && (values == null || !values.Contains(input)))
        {
            input = Console.ReadLine() ?? string.Empty;
        }
        
        return input;
    }
}