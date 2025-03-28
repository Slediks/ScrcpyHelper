using System.ComponentModel;

namespace ScrcpyHelper.Enums;

public enum AudioSrc
{
    [Description("output")]
    Output,
    
    [Description("mic")]
    Mic,
    
    [Description("playback")]
    Playback
}