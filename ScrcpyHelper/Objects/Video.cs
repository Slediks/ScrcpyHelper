using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Video : Properties
{
    public Properties Source { get; private set; } = new Display(); // Display | Camera
    public Orientation Orientation { get; } = new();

    private string GetSourceString() => $"Источник: {GetSourceName()}";
    private string GetOrientationString() => "Поворот:";

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

    public string GetSource()
    {
        return Source.ToString();
    }

    public string GetOrientation()
    {
        return Orientation.ToString();
    }
    
    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetSourceString()} {ChangeableOpenable}");
            props.Add($"{GetOrientation()} {Openable}");


            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    switch (result.Item2)
                    {
                        case Change:
                            SetSource();
                            break;
                        case Open:
                            Source.ChangeProps();
                            break;
                    }
                    break;
                case 2:
                    Orientation.ChangeProps();
                    break;
            }
        }
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine(GetSourceString())
            .AppendLine(new StringBuilder().AppendJoin('\n', GetSource().Split('\n').Select(s => "|  " + s)).ToString())
            .AppendLine(GetOrientationString())
            .Append(new StringBuilder().AppendJoin('\n', GetOrientation().Split('\n').Select(s => "|  " + s)))
            .ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        
        strings.Add($"--video-source={Source.GetType().Name.ToLower()}");

        var srcProps = Source.GetProperties();
        if (srcProps != string.Empty)
            strings.Add(srcProps);

        var oriProps = Orientation.GetProperties();
        if (oriProps != string.Empty)
            strings.Add(oriProps);

        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);

        return sb.ToString();
    }
}