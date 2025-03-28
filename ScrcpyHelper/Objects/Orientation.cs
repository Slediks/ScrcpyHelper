using System.Text;
using ScrcpyHelper.Enums;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Orientation : Properties
{
    private RotationDegrees Rotate { get; set; } = RotationDegrees.Zero;
    public bool Flip { get; set; }

    private string GetRotateString() => $"Градус: {GetRotate()}";
    private string GetFlipString() => $"Развернуть: {(Flip ? "Да" : "Нет")}";

    private static readonly List<RotationDegrees> RotationList = Enum.GetValues<RotationDegrees>().ToList();

    private static readonly List<string> RotationStringList =
        (from object? rot in RotationList select EnumHelper.GetDescription((RotationDegrees)rot)).ToList();

    public void SetRotate()
    {
        Rotate = RotationList[
            ConsoleWorker.WriteListAndReadNumber("Список доступных углов:", RotationStringList.ToArray())];
    }

    public string GetRotate()
    {
        return EnumHelper.GetDescription(Rotate);
    }
    
    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetRotateString()} {Changeable}");
            props.Add($"{GetFlipString()} {Changeable}");


            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    SetRotate();
                    break;
                case 2:
                    Flip = !Flip;
                    break;
            }
        }
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine(GetRotateString())
            .Append(GetFlipString())
            .ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        if (Rotate != RotationDegrees.Zero || Flip)
            strings.Add($"--orientation={(Flip ? "flip" : "")}{GetRotate()}");

        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);

        return sb.ToString();
    }
}