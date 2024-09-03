namespace Groova;

public partial class MainScene : Node
{
    public SongPlayer MusicPlayer;
    public bool Searching = false;
    public Playlist CurrentPlaylist = null;
    public bool InPlaylists = true;

    private PlaylistContainer playlistsContainer;
    private TopSection topSection;

    public override void Start()
    {
        MusicPlayer = GetChild<SongPlayer>();
        topSection = GetChild<TopSection>();
        playlistsContainer = GetChild<PlaylistContainer>();

        LoadPlaylists();
    }

    public void LoadPlaylists()
    {
        InPlaylists = true;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<SearchItemList>()?.Destroy();

        var playlistsItemList = GetChild<PlaylistItemList>("PlaylistItemList");
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
}