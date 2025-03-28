using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Video : IProperties
{
    public IProperties Source { get; private set; } = new Display(); // Display | Camera
    public Orientation Orientation { get; } = new();

    private static readonly List<string> SrcStrList = ["Дисплей", "Камера"];
    private int _currSrc;

    public void SetSource()
    {
        _currSrc = ConsoleWorker.WriteListAndReadNumber("Список доступных источников:", SrcStrList.ToArray());
        
        switch (_currSrc)
        {
            case 0:
                Source = new Display();
                return;
            case 1:
                Source = new Camera();
                return;
        }
    }

    public string GetSourceName()
    {
        return SrcStrList[_currSrc];
    }

    public string GetSourceString()
    {
        return Source.ToString();
    }

    public string GetOrientationString()
    {
        return Orientation.ToString();
    }
    
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"Источник: {GetSourceName()}")
            .AppendLine(new StringBuilder().AppendJoin('\n', GetSourceString().Split('\n').Select(s => "|  " + s)).ToString())
            .AppendLine("Поворот:")
            .Append(new StringBuilder().AppendJoin('\n', GetOrientationString().Split('\n').Select(s => "|  " + s)).ToString())
            .ToString();
    }

    public string GetProperties()
    {
        List<string> strings = [];
        strings.Add(Source.GetProperties());
        strings.Add(Orientation.GetProperties());
        
        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);
        
        return sb.ToString();
    }
}