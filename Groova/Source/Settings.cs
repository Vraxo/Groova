namespace Groova;

public class Settings
{
    public string Playlist { get; set; }
    public string Song { get; set; }
    public float Timestamp { get; set; } = 0;
    public float Pitch { get; set; } = 0.5f;
    public float Volume { get; set; } = 1f;
    public string ReplayMode { get; set; } = "Stop";
}