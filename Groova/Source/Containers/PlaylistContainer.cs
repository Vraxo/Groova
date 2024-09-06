using System.Text.Json;
using System.Text.RegularExpressions;

namespace Groova;

public class PlaylistContainer
{
    public List<Playlist> Playlists { get; set; } = [];

    private static PlaylistContainer? instance;

    public static PlaylistContainer Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    private PlaylistContainer()
    {
        Load();
    }

    private readonly string path = "Resources/Playlists.json";

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
        if (!File.Exists(path))
        {
            return;
        }

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
                .OrderBy(o => PadNumbers(o.FilePath))
                .ToList();
        }
    }

    public bool PlaylistExists(string name)
    {
        return Playlists.Any(p => p.Name == name);
    }

    public bool SongExists(string playlistName, string songFilePath)
    {
        // Find the playlist by name
        var playlist = Playlists.FirstOrDefault(p => p.Name == playlistName);

        // If the playlist exists, check if the song exists within it
        if (playlist != null)
        {
            return playlist.Songs.Any(s => s.FilePath == songFilePath);
        }

        // Return false if the playlist doesn't exist
        return false;
    }

    public Playlist GetPlaylist(string name)
    {
        // Return the first matching playlist, or null if not found
        return Playlists.FirstOrDefault(p => p.Name == name);
    }

    public Song GetSong(string playlistName, string songFilePath)
    {
        Playlist playlist = GetPlaylist(playlistName);

        if (playlist != null)
        {
            return playlist.Songs.FirstOrDefault(s => s.FilePath == songFilePath);
        }

        return null;
    }

    private static string PadNumbers(string input)
    {
        return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
    }
}