namespace Groova;

public class Playlist(string name)
{
    public string Name { get; set; } = name;
    public List<string> Paths { get; set; } = [];
}