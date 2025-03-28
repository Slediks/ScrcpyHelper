using System.Text;

namespace ScrcpyHelper.Objects;

public class Display : IProperties
{
    public Size? NewDisplay { get; private set; }
    public bool IsScreenOff { get; set; } = false;
    public bool IsControl { get; set; } = true;
    public App? App { get; private set; }

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
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Новый дисплей: {(NewDisplay != null ? "Да" : "Нет")}");
        
        if (NewDisplay != null)
            sb.AppendLine(new StringBuilder().AppendJoin('\n', GetNewDisplay().Split('\n').Select(s => "|  " + s))
                .ToString());
        
        sb.AppendLine($"Выключить экран: {(IsScreenOff ? "Да" : "Нет")}")
            .AppendLine($"Управление: {(IsControl ? "Да" : "Нет")}")
            .Append($"Приложение: {(App != null ? "Да" : "Нет")}");
        
        if (App != null)
            sb.Append('\n')
                .Append(new StringBuilder().AppendJoin('\n', GetApp().Split('\n').Select(s => "|  " + s)));
        
        return sb.ToString();
    }

    public string GetProperties()
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