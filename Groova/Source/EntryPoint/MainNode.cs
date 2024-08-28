using Raylib_cs;

namespace Groova;

public partial class MainScene : Node
{
    public MusicPlayer MusicPlayer;

    private PlaylistsContainer playlistsContainer;
    private bool inPlaylists = true;

    private TopSection topSection;

    public override void Start()
    {
        MusicPlayer = GetChild<MusicPlayer>();

        topSection = GetChild<TopSection>();

        playlistsContainer = GetChild<PlaylistsContainer>();

        LoadPlaylists();
    }

    public void LoadPlaylists()
    {
        var musicsItemlist = GetChild<ItemList>("MusicsList");
        musicsItemlist?.Destroy();

        ItemList playlistItemList = new()
        {
            ItemSize = new(100, 40),
            OnUpdate = (list) =>
            {
                float x = list.Position.X;
                float y = 50;
                list.Position = new(x, y);

                float width = Raylib.GetScreenWidth();
                float height = Raylib.GetScreenHeight() - list.Position.Y - 80;
                list.Size = new(width, height);
            }
        };

        AddChild(playlistItemList, "PlaylistsList");

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Text = playlist.Name,
                Playlist = playlist
            };
        
            playlistItemList.Add(playlistItem);
        }
    }

    public void LoadMusics(Playlist playlist)
    {
        topSection.InPlaylists = false;
        topSection.CurrentPlaylist = playlist;

        GetChild<ItemList>("PlaylistsList")?.Destroy();

        ItemList musicsList = new()
        {
            ItemSize = new(100, 40),
            OnUpdate = (list) =>
            {
                float x = list.Position.X;
                float y = 50;
                list.Position = new(x, y);

                float width = Raylib.GetScreenWidth();
                float height = Raylib.GetScreenHeight() - list.Position.Y - 80;
                list.Size = new(width, height);
            }
        };

        AddChild(musicsList, "MusicsList");

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