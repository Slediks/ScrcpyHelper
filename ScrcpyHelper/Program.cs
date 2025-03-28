using ScrcpyHelper.Helpers;
using ScrcpyHelper.Objects;

namespace ScrcpyHelper;

static class Program
{
    static async Task Main()
    {
        var window = new Window();
        while (true)
        {
            ConsoleWorker.WriteToConsole(window.ToString());

            switch (ConsoleWorker.WriteAndReadString("Желаете изменить параметры?", ["Y", "N", "Exit"], false))
            {
                case "Y":
                    window.ChangeProps();
                    break;
                case "N":
                    await DeviceWorker.Run(window.GetProperties());
                    break;
                case "Exit":
                    return;
            }
        }
    }
}