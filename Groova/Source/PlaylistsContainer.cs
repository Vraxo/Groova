using System.Text.Json;
using System.Text.RegularExpressions;

namespace Groova;

public class PlaylistsContainer : Node
{
    public List<Playlist> Playlists { get; set; } = [];

    private readonly string path = "Resources/Playlists.json";

    public void AddPlaylist(string name)
    {
        Playlist playlist = new(name);
        Playlists.Add(playlist);
        Save();
    }

    public void AddMusic(Playlist playlist, string musicPath)
    {
        playlist.Paths.Add(musicPath);
        Save();
    }

    public void Save()
    {
        Sort();
        string json = JsonSerializer.Serialize(Playlists, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string json = File.ReadAllText(path);
        Playlists = JsonSerializer.Deserialize<List<Playlist>>(json);
    }

    private static string PadNumbers(string input)
    {
        return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
    }

    private void Sort()
    {
        Playlists = Playlists
                    .OrderBy(o => PadNumbers(o.Name))
                    .ToList();
    }
}