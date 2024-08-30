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

    public void AddSong(Playlist playlist, string musicPath)
    {
        playlist.Songs.Add(new(musicPath));
        Save();
    }

    public void RemovePlaylist(Playlist playlist)
    {
        Playlists.Remove(playlist);
        Save();
    }

    public void RemoveSong(Playlist playlist, Song song)
    {
        playlist.Songs.Remove(song);
        Save();
    }

    public void SetPlaylistImage(Playlist playlist, string imagePath)
    {
        playlist.ImagePath = imagePath;
        Save();
    }

    public void SetSongImage(Song song, string imagePath)
    {
        song.ImagePath = imagePath;
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
            //playlist.Songs = playlist.Songs
            //.OrderBy(PadNumbers)
            //.ToList();

            playlist.Songs = playlist.Songs
                .OrderBy(o => PadNumbers(o.Path))
                .ToList();
        }
    }

    private static string PadNumbers(string input)
    {
        return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
    }
}