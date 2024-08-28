namespace Groova;

public partial class MainScene : Node
{
    public MusicPlayer MusicPlayer;

    private PlaylistsContainer playlistsContainer;
    private PlaylistItemlist playlistsList;
    private ItemList musicsList;
    private Playlist? currentPlaylist = null;
    private bool inPlaylists = true;

    private TopSection topSection;

    public override void Start()
    {
        MusicPlayer = GetChild<MusicPlayer>();

        topSection = GetChild<TopSection>();

        playlistsContainer = GetChild<PlaylistsContainer>();
        playlistsList = GetChild<PlaylistItemlist>("PlaylistsList");
        
        //musicsList = GetChild<ItemList>("MusicsList");
        //musicsList.Deactivate();

        LoadPlaylists();
    }

    public void LoadMusics(Playlist playlist)
    {
        topSection.InPlaylists = false;
        currentPlaylist = playlist;
        playlistsList.Deactivate();
        musicsList.Activate();
        musicsList.Clear();

        foreach (string path in playlist.Paths)
        {
            MusicItem musicItem = new()
            {
                MusicPath = path
            };

            musicsList.Add(musicItem);
        }
    }
}