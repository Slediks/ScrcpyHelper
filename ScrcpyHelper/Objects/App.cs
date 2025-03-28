using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class App : Properties
{
    private int Name { get; set; }
    public bool Save { get; set; } = true;

    private string GetNameString() => $"Название: {GetName()}";
    private string GetSaveString() => $"Сохранить: {(Save ? "Да" : "Нет")}";

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
        Name = ConsoleWorker.WriteListAndReadNumber("Список доступных приложений:", NamesDesc.ToArray());
    }

    public string GetName()
    {
        return NamesDesc[Name];
    }

    public string GetRealName()
    {
        return Names[Name];
    }

    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetNameString()} {Changeable}");
            props.Add($"{GetSaveString()} {Changeable}");


            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    SetName();
                    break;
                case 2:
                    Save = !Save;
                    break;
            }
        }
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine(GetNameString())
            .Append(GetSaveString())
            .ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        strings.Add($"--start-app={GetRealName()}");
        if (Save) strings.Add("--no-vd-destroy-content");

        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);

        return sb.ToString();
    }
}