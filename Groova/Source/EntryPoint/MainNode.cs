namespace Groova;

public partial class MainScene : Node
{
    public SongPlayer MusicPlayer;

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
        topSection.InPlaylists = true;

        var songsItemList = GetChild<ItemList>("SongItemList");
        songsItemList?.Destroy();

        var playlistsItemList = GetChild<PlaylistItemList>("PlaylistItemList");
        playlistsItemList?.Destroy();

        PlaylistItemList playlistItemList = new();
        AddChild(playlistItemList);
    }

    public void LoadMusics(Playlist playlist)
    {
        topSection.InPlaylists = false;
        topSection.CurrentPlaylist = playlist;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<ItemList>("PlaylistItemList")?.Destroy();

        SongItemList songItemList = new()
        {
            Playlist = playlist
        };

        AddChild(songItemList);
    }
}