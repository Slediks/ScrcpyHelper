using System.Text;
using ScrcpyHelper.Enums;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Audio : Properties
{
    private AudioSrc Source { get; set; } = AudioSrc.Output;

    private string GetSourceString() => $"Источник: {GetSource()}";

    private static readonly List<AudioSrc> SrcList = Enum.GetValues<AudioSrc>().ToList();

    private static readonly List<string> SrcStrList =
        (from object? src in SrcList select EnumHelper.GetDescription((AudioSrc)src)).ToList();

    public void SetSource()
    {
        Source = SrcList[ConsoleWorker.WriteListAndReadNumber("Список доступных источников:", SrcStrList.ToArray())];
    }

    public string GetSource()
    {
        return EnumHelper.GetDescription(Source);
    }
    
    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetSourceString()} {Changeable}");

            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    SetSource();
                    break;
            }
        }
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append(GetSourceString())
            .ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        strings.Add($"--audio-source={GetSource()}");

        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);

        return sb.ToString();
    }
}