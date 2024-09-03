namespace Groova;

public partial class MainScene : Node
{
    public SongPlayer MusicPlayer;

    private bool inPlaylists = true;
    private Playlist currentPlaylist = null;
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
        inPlaylists = true;
        topSection.InPlaylists = true;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<SearchItemList>()?.Destroy();

        var playlistsItemList = GetChild<PlaylistItemList>("PlaylistItemList");
        playlistsItemList?.Destroy();

        PlaylistItemList playlistItemList = new();
        AddChild(playlistItemList);
    }

    public void LoadSongs(Playlist playlist)
    {
        inPlaylists = false;
        currentPlaylist = playlist;
        topSection.InPlaylists = false;
        topSection.CurrentPlaylist = playlist;

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
        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<ItemList>("PlaylistItemList")?.Destroy();

        SearchItemList searchItemList = new();
        AddChild(searchItemList);
    }

    public void StopSearch()
    {
        GetChild<SearchItemList>()?.Destroy();

        if (inPlaylists)
        {
            LoadPlaylists();
        }
        else
        {
            LoadSongs(currentPlaylist);
        }
    }
}