using System.Text;
using ScrcpyHelper.Enums;

namespace ScrcpyHelper.Objects;

public class Window : IProperties
{
    public string Title { get; set; } = "My Device";
    public bool IsBorder { get; set; } = true;
    public bool IsAlwaysOnTop { get; set; } = false;
    public bool IsFullScreen { get; set; } = false;
    public Video? Video { get; private set; } = new();
    public Audio? Audio { get; private set; } = new();


    public void SetVideo()
    {
        Video = Video != null ? null : new Video();
    }

    public string GetVideoString()
    {
        return Video != null ? Video.ToString() : string.Empty;
    }

    public void SetAudio()
    {
        Audio = Audio != null ? null : new Audio();
    }

    public string GetAudioString()
    {
        return Audio != null ? Audio.ToString() : string.Empty;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Название: {Title}")
            .AppendLine($"Границы окна: {(IsBorder ? "Да" : "Нет")}")
            .AppendLine($"Поверх окон: {(IsAlwaysOnTop ? "Да" : "Нет")}")
            .AppendLine($"Полный экран: {(IsFullScreen ? "Да" : "Нет")}")
            .AppendLine($"Изображение: {(Video != null ? "Да" : "Нет")}");

        if (Video != null)
            sb.AppendLine(new StringBuilder().AppendJoin('\n', GetVideoString().Split('\n').Select(s => "|  " + s))
                .ToString());
        
        sb.Append($"Звук: {(Audio != null ? "Да" : "Нет")}");

        if (Audio != null)
            sb.Append('\n')
                .Append(new StringBuilder().AppendJoin('\n', GetAudioString().Split('\n').Select(s => "|  " + s))
);

        return sb.ToString();
    }

    public string GetProperties()
    {
        List<string> strings = [];
        strings.Add($"--window-title='{Title}'");
        if (!IsBorder) strings.Add("--window-borderless");
        if (IsAlwaysOnTop) strings.Add("--always-on-top");
        if (IsFullScreen) strings.Add("--fullscreen");
        
        var videoProps = Video != null ? Video.GetProperties() : "--no-video";
        if (videoProps != string.Empty)
            strings.Add(videoProps);
        
        var audioProps = Audio != null ? Audio.GetProperties() : "--no-audio";
        if (audioProps != string.Empty)
            strings.Add(audioProps);
        
        var sb = new StringBuilder();
        sb.AppendJoin(' ', strings);
        
        return sb.ToString();
    }
}