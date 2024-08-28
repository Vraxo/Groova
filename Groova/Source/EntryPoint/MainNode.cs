namespace Groova;

public partial class MainScene : Node
{
    public MusicPlayer MusicPlayer;

    private PlaylistsContainer playlistsContainer;
    private ItemList playlistsList;
    private ItemList musicsList;
    private Playlist? currentPlaylist = null;
    private bool inPlaylists = true;

    private TopSection topSection;

    public override void Start()
    {
        MusicPlayer = GetChild<MusicPlayer>();

        topSection = GetChild<TopSection>();

        playlistsContainer = GetChild<PlaylistsContainer>();
        playlistsList = GetChild<ItemList>("PlaylistsList");
        
        musicsList = GetChild<ItemList>("MusicsList");
        musicsList.Deactivate();

        LoadPlaylists();
    }

    private void OnPlaylistButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistButton = sender as PlaylistButton;
        Playlist playlist = playlistButton.Playlist;
        LoadMusics(playlist);
    }

    public void LoadPlaylists()
    {
        musicsList.Deactivate();
        playlistsList.Activate();
        playlistsList.Clear();
        playlistsContainer.Load();

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Text = playlist.Name,
                Playlist = playlist
            };

            playlistsList.Add(playlistItem);
        }
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