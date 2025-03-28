namespace ScrcpyHelper.Helpers;

public static class DeviceWorker
{
    public static string[] GetListOfApps()
    {
        var output = GetOutput("--list-apps");
        var outputArray = output
            .Split("List of apps:")[1]
            .Split("\n *")
            .Skip(1)
            .SelectMany(s => s.Split("\n -"))
            .Select(s => s.Trim())
            .ToArray();
        
        for (var i = 0; i < outputArray.Length; i++)
        {
            while (outputArray[i].Contains("  "))
            {
                outputArray[i] = outputArray[i].Replace("  ", " ");
            }
            
            var splitApp = outputArray[i].Split(" ").Select(s => s.Trim()).ToArray();
            var nameApp = string.Join(" ", splitApp.SkipLast(1));
            var packageApp = splitApp.Last();
            
            outputArray[i] = string.Join("\t", nameApp, packageApp);
        }
        
        return outputArray;
    }

    public static string[] GetListOfCameras()
    {
        var output = GetOutput("--list-cameras");
        var outputArray = output
            .Split("List of cameras:")[1]
            .Split("\n ")
            .Skip(1)
            .Select(s => s.Trim())
            .ToArray();

        for (var i = 0; i < outputArray.Length; i++)
        {
            while (outputArray[i].Contains("  "))
            {
                outputArray[i] = outputArray[i].Replace("  ", " ");
            }

            outputArray[i] = outputArray[i].Replace("--camera-id=", "");
            var splitCamera = outputArray[i].Split(" ");
            var idCamera = splitCamera.First();
            var propsCamera = string.Join(" ", splitCamera.Skip(1)).Replace("(", "").Replace(")", "");

            outputArray[i] = string.Join("\t", idCamera, propsCamera);
        }

        return outputArray;
    }

    public static async Task Run(string args)
    {
        Console.Clear();
        try
        {
            using System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = @"..\scrcpy.exe";
            pProcess.StartInfo.Arguments = args; //argument
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = false;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            pProcess.StartInfo.CreateNoWindow = false; //not display a windows
            pProcess.Start();
            await pProcess.WaitForExitAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private static string GetOutput(string arg)
    {
        using System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
        pProcess.StartInfo.FileName = @"..\scrcpy.exe";
        pProcess.StartInfo.Arguments = arg; //argument
        pProcess.StartInfo.UseShellExecute = false;
        pProcess.StartInfo.RedirectStandardOutput = true;
        pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        pProcess.StartInfo.CreateNoWindow = false; //not display a windows
        pProcess.Start();
        string output = pProcess.StandardOutput.ReadToEnd(); //The output result
        pProcess.WaitForExit();
        
        return output;
    }
}