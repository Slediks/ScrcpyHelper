using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Size(int width, int height) : Properties
{
    public int Width { get; private set; } = width;
    public int Height { get; private set; } = height;

    private string GetWidthString() => $"Ширина: {Width}";
    private string GetHeightString() => $"Высота: {Height}";

    public void SetWidth()
    {
        Width = ConsoleWorker.WriteAndReadNumber("Введите ширину:", 50, 5000);
    }

    public void SetHeight()
    {
        Height = ConsoleWorker.WriteAndReadNumber("Введите высоту:", 50, 5000);
    }
    
    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetWidthString()} {Changeable}");
            props.Add($"{GetHeightString()} {Changeable}");


            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    SetWidth();
                    break;
                case 2:
                    SetHeight();
                    break;
            }
        }
    }

    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine(GetWidthString())
            .Append(GetHeightString())
            .ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        strings.Add(Width.ToString());
        strings.Add(Height.ToString());

        var sb = new StringBuilder();
        sb.AppendJoin('x', strings);

        return sb.ToString();
    }
}