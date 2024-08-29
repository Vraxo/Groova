using System.Text.Json;
using System.Text.RegularExpressions;

namespace Groova;

public class PlaylistsContainer : Node
{
    public List<Playlist> Playlists { get; set; } = [];

    private readonly string path = "Resources/Playlists.json";

    public override void Start()
    {
        Load();
    }

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

    public void RemoveMusic(Playlist playlist, string musicPath)
    {
        playlist.Paths.Remove(musicPath);
        Save();
    }

    public void Save()
    {
        Sort();

        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(Playlists, options);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string json = File.ReadAllText(path);
        Playlists = JsonSerializer.Deserialize<List<Playlist>>(json);
    }

    private void Sort()
    {
        SortPlaylists();
        SortSongs();
    }

    private void SortPlaylists()
    {
        Playlists = Playlists
            .OrderBy(o => PadNumbers(o.Name))
            .ToList();
    }

    private void SortSongs()
    {
        foreach (Playlist playlist in Playlists)
        {
            playlist.Paths = playlist.Paths
                .OrderBy(PadNumbers)
                .ToList();
        }
    }

    private static string PadNumbers(string input)
    {
        return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
    }
}