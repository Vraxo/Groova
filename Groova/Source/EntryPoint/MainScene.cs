using System.Text.Json;

namespace Groova;

public partial class MainScene : Node
{
    public bool Searching = false;
    public bool InPlaylists = true;
    public Playlist CurrentPlaylist = null;
    public SongPlayer SongPlayer;
    public Settings Settings = new();

    private readonly string settingsFilePath = "Resources/Settings.json";

    public override void Ready()
    {
        SongPlayer = GetNode2<SongPlayer>("SongPlayer");

        LoadPlaylists();
        LoadSettings();
    }

    public void LoadPlaylists()
    {
        InPlaylists = true;

        GetNode2<TextBox>("/root/TopSection/SearchBar").Text = "";

        GetNode2<ItemList>("SongItemList")?.Destroy();
        GetNode2<SearchItemList>("SearchItemList")?.Destroy();

        var playlistsItemList = GetNode2<PlaylistItemList>("PlaylistItemList");
        playlistsItemList?.Destroy();

        PlaylistItemList playlistItemList = new();
        AddChild(playlistItemList);
    }

    public void LoadSongs(Playlist playlist)
    {
        InPlaylists = false;
        CurrentPlaylist = playlist;
        SongPlayer.Playlist = playlist;

        GetNode2<TextBox>("/root/TopSection/SearchBar").Text = "";

        GetNode2<ItemList>("SongItemList")?.Destroy();
        GetNode2<ItemList>("PlaylistItemList")?.Destroy();
        GetNode2<SearchItemList>("SearchItemList")?.Destroy();

        SongItemList songItemList = new()
        {
            Playlist = playlist
        };

        AddChild(songItemList);
    }

    public void Search()
    {
        Searching = true;

        GetNode2<ItemList>("SongItemList")?.Destroy();
        GetNode2<ItemList>("PlaylistItemList")?.Destroy();

        SearchItemList searchItemList = new();
        AddChild(searchItemList);
    }

    public void StopSearch()
    {
        Searching = false;

        GetChild<SearchItemList>()?.Destroy();
        GetNode2<TextBox>("/root/TopSection/SearchBar").Text = "";

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

        File.WriteAllText(settingsFilePath, jsonString);
    }

    private void LoadSettings()
    {
        if (!File.Exists(settingsFilePath))
        {
            return;
        }

        string jsonString = File.ReadAllText(settingsFilePath);
        Settings = JsonSerializer.Deserialize<Settings>(jsonString) ?? new();

        var currentSongDisplayer = GetNode2<CurrentSongDisplayer>("BottomSection/CurrentSongDisplayer");
        currentSongDisplayer.Button.Text = Settings.ReplayMode;
        SongPlayer.ReplayMode = Settings.ReplayMode;

        var bottomSection = GetChild<BottomSection>();
        bottomSection.LoadSettings(Settings);

        if (Settings.Playlist != null)
        {
            LoadSongs(Settings.Playlist);

            if (Settings.Song != null)
            {
                bottomSection.GetChild<CurrentSongDisplayer>().SetSong(Settings.Song);
                SongPlayer.Load(Settings.Song.FilePath);
                SongPlayer.Play();
                SongPlayer.Seek(Settings.Timestamp * SongPlayer.AudioLength);
                SongPlayer.Pause();
            }
        }
    }
}