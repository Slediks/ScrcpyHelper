using System.Text;
using ScrcpyHelper.Enums;
using ScrcpyHelper.Helpers;

namespace ScrcpyHelper.Objects;

public class Window : Properties
{
    public string Title { get; set; } = "My Device";
    public bool IsBorder { get; set; } = true;
    public bool IsAlwaysOnTop { get; set; } = false;
    public bool IsFullScreen { get; set; } = false;
    public Video? Video { get; private set; } = new();
    public Audio? Audio { get; private set; } = new();

    private string GetTitleString() => $"Название: {Title}";
    private string GetIsBorderString() => $"Границы окна: {(IsBorder ? "Да" : "Нет")}";
    private string GetIsAlwaysOnTopString() => $"Поверх окон: {(IsAlwaysOnTop ? "Да" : "Нет")}";
    private string GetIsFullScreenString() => $"Полный экран: {(IsFullScreen ? "Да" : "Нет")}";
    private string GetVideoString() => $"Изображение: {(Video != null ? "Да" : "Нет")}";
    private string GetAudioString() => $"Звук: {(Audio != null ? "Да" : "Нет")}";

    public void SetTitle()
    {
        Title = ConsoleWorker.WriteAndReadString("Введите название окна:");
    }
    
    public void SetVideo()
    {
        Video = Video != null ? null : new Video();
    }

    public string GetVideo()
    {
        return Video != null ? Video.ToString() : string.Empty;
    }

    public void SetAudio()
    {
        Audio = Audio != null ? null : new Audio();
    }

    public string GetAudio()
    {
        return Audio != null ? Audio.ToString() : string.Empty;
    }
    
    public override void ChangeProps()
    {
        while (true)
        {
            List<string> props = [];
            props.Add($"{GetTitleString()} {Changeable}");
            props.Add($"{GetIsBorderString()} {Changeable}");
            props.Add($"{GetIsAlwaysOnTopString()} {Changeable}");
            props.Add($"{GetIsFullScreenString()} {Changeable}");
            props.Add($"{GetVideoString()} {(Video != null ? ChangeableOpenable : Changeable)}");
            props.Add($"{GetAudioString()} {(Audio != null ? ChangeableOpenable : Changeable)}");


            var result = ConsoleWorker.WriteListAndRead2Args(props.ToArray());
            switch (result.Item1)
            {
                case 0:
                    return;
                case 1:
                    SetTitle();
                    break;
                case 2:
                    IsBorder = !IsBorder;
                    break;
                case 3:
                    IsAlwaysOnTop = !IsAlwaysOnTop;
                    break;
                case 4:
                    IsFullScreen = !IsFullScreen;
                    break;
                case 5:
                    switch (result.Item2)
                    {
                        case Change:
                            SetVideo();
                            break;
                        case Open:
                            Video?.ChangeProps();
                            break;
                    }
                    break;
                case 6:
                    switch (result.Item2)
                    {
                        case Change:
                            SetAudio();
                            break;
                        case Open:
                            Audio?.ChangeProps();
                            break;
                    }
                    break;
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(GetTitleString())
            .AppendLine(GetIsBorderString())
            .AppendLine(GetIsAlwaysOnTopString())
            .AppendLine(GetIsFullScreenString())
            .AppendLine(GetVideoString());

        if (Video != null)
            sb.AppendLine(new StringBuilder().AppendJoin('\n', GetVideo().Split('\n').Select(s => "|  " + s))
                .ToString());

        sb.Append(GetAudioString());

        if (Audio != null)
            sb.Append('\n')
                .Append(new StringBuilder().AppendJoin('\n', GetAudio().Split('\n').Select(s => "|  " + s)));

        return sb.ToString();
    }

    public override string GetProperties()
    {
        List<string> strings = [];
        strings.Add($"--window-title=\"{Title}\"");
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