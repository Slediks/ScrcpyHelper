using System.ComponentModel;
using System.Text;
using ScrcpyHelper.Enums;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Camera : IProperties
{
    private object Id { get; set; } = 0; // CameraFacing | int
    public Size Size { get; } = new(600, 600);
    public int Fps { get; set; } = 30;

    private static readonly List<int> Ids = [];
    private static readonly List<string> IdsDesc = DeviceWorker.GetListOfCameras().Select(s =>
    {
        var split = s.Split('\t');
        Ids.Add(int.Parse(split[0]));
            
        return split[1];
    }).ToList();

    private static readonly List<CameraFacing> FaceList = Enum.GetValues<CameraFacing>().ToList();

    private static readonly List<string> FaceStrList =
        (from object? face in FaceList select EnumHelper.GetDescription((CameraFacing)face)).ToList();

    public void SetId()
    {
        switch (ConsoleWorker.WriteListAndReadNumber("Доступные способы установки камеры:", ["Тип камеры", "Список камер"]))
        {
            case 0:
                Id = SetIdFace();
                return;
            case 1:
                Id = SetIdInt();
                return;
        }
    }

    public string GetSize()
    {
        return Size.ToString();
    }

    public string GetId()
    {
        return Id.GetType() == typeof(CameraFacing) ? EnumHelper.GetDescription((CameraFacing)Id) : IdsDesc[(int)Id];
    }

    public string GetRealId()
    {
        return Id.GetType() == typeof(CameraFacing) ? EnumHelper.GetDescription((CameraFacing)Id) : ((int)Id).ToString();
    }

    private static CameraFacing SetIdFace()
    {
        return FaceList[ConsoleWorker.WriteListAndReadNumber("Список типов камер:", FaceStrList.ToArray())];
    }

    private static int SetIdInt()
    {
        return Ids[ConsoleWorker.WriteListAndReadNumber("Список доступных камер:", IdsDesc.ToArray())];
    }
    
    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine($"Камера: {GetId()}")
            .AppendLine("Размер:")
            .AppendLine(new StringBuilder().AppendJoin('\n', GetSize().Split('\n').Select(s => "|  " + s)).ToString())
            .Append($"FPS: {Fps}")
            .ToString();
    }

    public string GetProperties()
    {
        var id = GetRealId();

        List<string> strings = [];
        strings.Add($"{(int.TryParse(id, out var result) ? $"--camera-id={result}" : $"--camera-facing={id}")}");
        strings.Add($"--camera-size={Size.GetProperties()}");
        strings.Add($"--camera-fps={Fps}");
        
        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);
        
        return sb.ToString();
    }
}