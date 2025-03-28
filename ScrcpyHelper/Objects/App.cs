using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class App : IProperties
{
    private string Name { get; set; } = string.Empty;
    public bool Save { get; set; } = true;

    private static readonly List<string> Names = [];

    private static readonly List<string> NamesDesc = DeviceWorker.GetListOfApps().Select(s =>
    {
        var split = s.Split('\t');
        Names.Add(split[1]);

        return split[0];
    }).ToList();

    public App()
    {
        SetName();
    }

    public void SetName()
    {
        Name = Names[ConsoleWorker.WriteListAndReadNumber("Список доступных приложений:", NamesDesc.ToArray())];
    }

    public string GetName()
    {
        return NamesDesc[Names.IndexOf(Name)];
    }

    public string GetRealName()
    {
        return Name;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"Название: {GetName()}")
            .Append($"Сохранить: {(Save ? "Да" : "Нет")}")
            .ToString();
    }

    public string GetProperties()
    {
        List<string> strings = [];
        strings.Add($"--start-app={Name}");
        if (Save) strings.Add("--no-vd-destroy-content");
        
        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);

        return sb.ToString();
    }
}