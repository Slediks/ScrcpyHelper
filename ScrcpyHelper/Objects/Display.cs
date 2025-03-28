using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Display : Properties
{
    public Size? NewDisplay { get; private set; }
    public bool IsScreenOff { get; set; } = false;
    public bool IsControl { get; set; } = true;
    public App? App { get; private set; }

    private string GetNewDisplayString() => $"Новый дисплей: {(NewDisplay != null ? "Да" : "Нет")}";
    private string GetIsScreenOffString() => $"Выключить экран: {(IsScreenOff ? "Да" : "Нет")}";
    private string GetIsControlString() => $"Управление: {(IsControl ? "Да" : "Нет")}";
    private string GetAppString() => $"Приложение: {(App != null ? "Да" : "Нет")}";

    public void SetNewDisplay()
    {
        NewDisplay = NewDisplay != null ? null : new Size(1920, 1080);
    }

    public string GetNewDisplay()
    {
        return NewDisplay?.ToString() ?? string.Empty;
    }

    public void SetApp()
    {
        App = App != null ? null : new App();
    }

    public string GetApp()
    {
        return App?.ToString() ?? string.Empty;
    }
    
    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetNewDisplayString()} {(NewDisplay != null ? ChangeableOpenable : Changeable)}");
            props.Add($"{GetIsScreenOffString()} {Changeable}");
            props.Add($"{GetIsControlString()} {Changeable}");
            props.Add($"{GetAppString()} {(App != null ? ChangeableOpenable : Changeable)}");


            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    switch (result.Item2)
                    {
                        case Change:
                            SetNewDisplay();
                            break;
                        case Open:
                            NewDisplay?.ChangeProps();
                            break;
                    }
                    break;
                case 2:
                    IsScreenOff = !IsScreenOff;
                    break;
                case 3:
                    IsControl = !IsControl;
                    break;
                case 4:
                    switch (result.Item2)
                    {
                        case Change:
                            SetApp();
                            break;
                        case Open:
                            App?.ChangeProps();
                            break;
                    }
                    break;
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(GetNewDisplayString());

        if (NewDisplay != null)
            sb.AppendLine(new StringBuilder().AppendJoin('\n', GetNewDisplay().Split('\n').Select(s => "|  " + s))
                .ToString());

        sb.AppendLine(GetIsScreenOffString())
            .AppendLine(GetIsControlString())
            .Append(GetAppString());

        if (App != null)
            sb.Append('\n')
                .Append(new StringBuilder().AppendJoin('\n', GetApp().Split('\n').Select(s => "|  " + s)));

        return sb.ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        if (NewDisplay != null) strings.Add($"--new-display={NewDisplay.GetProperties()}");
        if (IsScreenOff) strings.Add("--turn-screen-off");
        if (!IsControl) strings.Add("--no-control");

        if (App != null)
        {
            var appProps = App.GetProperties();
            if (appProps != string.Empty)
                strings.Add(appProps);
        }

        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);

        return sb.ToString();
    }
}