using System.Text;
using ScrcpyHelper.Enums;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Audio : IProperties
{
    private AudioSrc Source { get; set; } = AudioSrc.Output;

    private static readonly List<AudioSrc> SrcList = Enum.GetValues<AudioSrc>().ToList();
    private static readonly List<string> SrcStrList =
        (from object? src in SrcList select EnumHelper.GetDescription((AudioSrc)src)).ToList();

    public void SetSource()
    {
        Source = SrcList[ConsoleWorker.WriteListAndReadNumber("Список доступных источников:", SrcStrList.ToArray())];
    }

    public string GetSourceString()
    {
        return EnumHelper.GetDescription(Source);
    }
    
    public override string ToString()
    {
        return new StringBuilder()
            .Append($"Источник: {GetSourceString()}")
            .ToString();
    }

    public string GetProperties()
    {
        List<string> strings = [];
        strings.Add($"--audio-src={GetSourceString()}");
        
        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);
        
        return sb.ToString();
    }
}