using System.Text;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Size(int width, int height) : IProperties
{
    public int Width { get; private set; } = width;
    public int Height { get; private set; } = height;

    public void SetWidth()
    {
        Width = ConsoleWorker.WriteAndReadNumber("Введите ширину:", 50, 5000);
    }
    
    public void SetHeight()
    {
        Height = ConsoleWorker.WriteAndReadNumber("Введите высоту:", 50, 5000);
    }
    
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"Ширина: {Width}")
            .Append($"Высота: {Height}")
            .ToString();
    }

    public string GetProperties()
    {
        List<string> strings = [];
        strings.Add(Width.ToString());
        strings.Add(Height.ToString());
        
        var sb = new StringBuilder();
        sb.AppendJoin('x', strings);
        
        return sb.ToString();
    }
}