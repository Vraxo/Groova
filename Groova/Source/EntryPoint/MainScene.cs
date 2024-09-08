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
        SongPlayer = GetNode<SongPlayer>("SongPlayer");

        LoadPlaylists();
        LoadSettings();
    }

    public void LoadPlaylists()
    {
        InPlaylists = true;

        GetNode<TextBox>("/root/TopSection/SearchBar").Text = "";

        GetNode<ItemList>("SongItemList")?.Destroy();
        GetNode<SearchItemList>("SearchItemList")?.Destroy();

        var playlistsItemList = GetNode<PlaylistItemList>("PlaylistItemList");
        playlistsItemList?.Destroy();

        PlaylistItemList playlistItemList = new();
        AddChild(playlistItemList);
    }

    public void LoadSongs(Playlist playlist)
    {
        InPlaylists = false;
        CurrentPlaylist = playlist;
        SongPlayer.Playlist = playlist;

        GetNode<TextBox>("/root/TopSection/SearchBar").Text = "";

        GetNode<ItemList>("SongItemList")?.Destroy();
        GetNode<ItemList>("PlaylistItemList")?.Destroy();
        GetNode<SearchItemList>("SearchItemList")?.Destroy();

        SongItemList songItemList = new()
        {
            Playlist = playlist
        };

        AddChild(songItemList);
    }

    public void Search()
    {
        Searching = true;

        GetNode<ItemList>("SongItemList")?.Destroy();
        GetNode<ItemList>("PlaylistItemList")?.Destroy();

        SearchItemList searchItemList = new();
        AddChild(searchItemList);
    }

    public void StopSearch()
    {
        Searching = false;

        GetNode<SearchItemList>("SearchItemList")?.Destroy();
        GetNode<TextBox>("/root/TopSection/SearchBar").Text = "";

        if (InPlaylists)
        {
            LoadPlaylists();
        }
        else
        {
            //LoadSongs(CurrentPlaylist);
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

        var currentSongDisplayer = GetNode<CurrentSongDisplayer>("BottomSection/CurrentSongDisplayer");
        currentSongDisplayer.Button.Text = Settings.ReplayMode;
        SongPlayer.ReplayMode = Settings.ReplayMode;

        bool setAudioSlider = false;

        bool playlistExists = PlaylistContainer.Instance.PlaylistExists(Settings.Playlist);

        if (Settings.Playlist != null && playlistExists)
        {
            Playlist playlist = PlaylistContainer.Instance.GetPlaylist(Settings.Playlist);

            LoadSongs(playlist);

            bool songExists = PlaylistContainer.Instance.SongExists(Settings.Playlist, Settings.Song);

            if (Settings.Song != null && songExists)
            {
                Song song = PlaylistContainer.Instance.GetSong(playlist.Name, Settings.Song);

                GetNode<CurrentSongDisplayer>("BottomSection/CurrentSongDisplayer").SetSong(song);

                SongPlayer.Load(song.FilePath);
                SongPlayer.Play();
                SongPlayer.Seek(Settings.Timestamp * SongPlayer.AudioLength);
                SongPlayer.Pause();

                setAudioSlider = true;
            }
        }

        var bottomSection = GetNode<BottomSection>("BottomSection");
        bottomSection.LoadSettings(Settings, setAudioSlider);
    }
}