using System.Text;
using ScrcpyHelper.Enums;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Orientation : IProperties
{
    private RotationDegrees Rotate { get; set; } = RotationDegrees.Zero;
    public bool Flip { get; set; } = false;

    private static readonly List<RotationDegrees> RotationList = Enum.GetValues<RotationDegrees>().ToList();

    private static readonly List<string> RotationStringList =
        (from object? rot in RotationList select EnumHelper.GetDescription((RotationDegrees)rot)).ToList();
    
    public void SetRotate()
    {
        Rotate = RotationList[ConsoleWorker.WriteListAndReadNumber("Список доступных углов:", RotationStringList.ToArray())];
    }

    public string GetRotate()
    {
        return EnumHelper.GetDescription(Rotate);
    }
    
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"Градус: {GetRotate()}")
            .Append($"Развернуть: {(Flip ? "Да" : "Нет")}")
            .ToString();
    }

    public string GetProperties()
    {
        List<string> strings = [];
        if (Rotate != RotationDegrees.Zero || Flip)
            strings.Add($"--orientation={(Flip ? "flip" : "")}{GetRotate()}");
        
        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);
        
        return sb.ToString();
    }
}