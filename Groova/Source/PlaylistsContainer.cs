using System.Text.Json;

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

    public void Save()
    {
        try
        {
            // Serialize the playlists to a JSON string
            string json = JsonSerializer.Serialize(Playlists, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to a file
            File.WriteAllText(path, json);
            Console.WriteLine($"Playlists saved to {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save playlists: {ex.Message}");
        }
    }

    // Method to load playlists from a JSON file
    public void Load()
    {
        try
        {
            // Read the JSON string from the file
            string json = File.ReadAllText(path);

            // Deserialize the JSON string back to the playlists list
            Playlists = JsonSerializer.Deserialize<List<Playlist>>(json);

            Console.WriteLine($"Playlists loaded from {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load playlists: {ex.Message}");
        }
    }
}