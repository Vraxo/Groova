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
        //topSection = GetChild<TopSection>();
        //playlistsContainer = GetChild<PlaylistContainer>();

        LoadPlaylists();
        LoadSettings();
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
        SongPlayer.Playlist = playlist;

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
        GetNode2<TextBox>("TopSection/SearchBar").Text = "";

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

        File.WriteAllText("Resources/Settings.json", jsonString);
    }

    private void LoadSettings()
    {
        const string settingsFilePath = "Resources/Settings.json";

        if (!File.Exists(settingsFilePath))
        {
            return;
        }

        string jsonString = File.ReadAllText(settingsFilePath);
        Settings = JsonSerializer.Deserialize<Settings>(jsonString) ?? new();

        var currentSongDisplayer = GetNode<CurrentSongDisplayer>("BottomSection/CurrentSongDisplayer");
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