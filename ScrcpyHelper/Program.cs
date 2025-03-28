using ScrcpyHelper.Helpers;
using ScrcpyHelper.Objects;

namespace ScrcpyHelper;

static class Program
{
    static void Main(string[] args)
    {
        var window = new Window();
        Console.WriteLine(window.ToString());
        Console.WriteLine(window.GetProperties());
    }
}