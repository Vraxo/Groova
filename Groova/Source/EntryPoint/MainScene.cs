using System.IO;
using System.Text.Json;

namespace Groova;

public partial class MainScene : Node
{
    public bool Searching = false;
    public bool InPlaylists = true;
    public Playlist CurrentPlaylist = null;
    public SongPlayer SongPlayer;
    public Settings Settings = new();

    private PlaylistContainer playlistsContainer;
    private TopSection topSection;

    public override void Ready()
    {
        SongPlayer = GetChild<SongPlayer>();
        topSection = GetChild<TopSection>();
        playlistsContainer = GetChild<PlaylistContainer>();

        LoadPlaylists();
        LoadSettings(); // Load settings when the scene starts
    }

    public void LoadPlaylists()
    {
        InPlaylists = true;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<SearchItemList>()?.Destroy();

        var playlistsItemList = GetChild<PlaylistItemList>();
        playlistsItemList?.Destroy();

        PlaylistItemList playlistItemList = new();
        AddChild(playlistItemList);
    }

    public void LoadSongs(Playlist playlist)
    {
        InPlaylists = false;
        CurrentPlaylist = playlist;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<ItemList>("PlaylistItemList")?.Destroy();
        GetChild<SearchItemList>()?.Destroy();

        GetNode<TextBox>("TopSection/SearchBar").Text = "";

        SongItemList songItemList = new()
        {
            Playlist = playlist
        };

        AddChild(songItemList);
    }

    public void Search()
    {
        Searching = true;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<ItemList>("PlaylistItemList")?.Destroy();

        SearchItemList searchItemList = new();
        AddChild(searchItemList);
    }

    public void StopSearch()
    {
        Searching = false;

        GetChild<SearchItemList>()?.Destroy();

        if (InPlaylists)
        {
            LoadPlaylists();
        }
        else
        {
            LoadSongs(CurrentPlaylist);
        }
    }

    public void SaveSettings()
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(Settings, options);

        File.WriteAllText("Settings.json", jsonString);
    }

    private void LoadSettings()
    {
        if (File.Exists("Settings.json"))
        {
            string jsonString = File.ReadAllText("Settings.json");

            Settings = JsonSerializer.Deserialize<Settings>(jsonString) ??
                       new();

            if (Settings.Playlist is null)
            {
                return;
            }

            LoadSongs(Settings.Playlist);
        
            if (Settings.Song is null)
            {
                return;
            }

            SongPlayer.Load(Settings.Song.FilePath);

            var bottomSection = GetChild<BottomSection>();
            bottomSection.LoadSettings(Settings);

            SongPlayer.Play();
            SongPlayer.Seek(Settings.Timestamp * SongPlayer.AudioLength);
            SongPlayer.Pause();
        }
    }
}