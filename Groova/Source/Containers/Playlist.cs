namespace Groova;

public class Playlist(string name)
{
    public string Name { get; set; } = name;
    public string ImagePath { get; set; }
    public List<Song> Songs { get; set; } = [];
}