using Raylib_cs;

namespace Groova;

public partial class MainScene : Node
{
    public SongPlayer MusicPlayer;

    private PlaylistsContainer playlistsContainer;
    private TopSection topSection;

    public override void Start()
    {
        MusicPlayer = GetChild<SongPlayer>();
        topSection = GetChild<TopSection>();
        playlistsContainer = GetChild<PlaylistsContainer>();

        LoadPlaylists();
    }

    public void LoadPlaylists()
    {
        topSection.InPlaylists = true;

        var songsItemList = GetChild<ItemList>("SongItemList");
        songsItemList?.Destroy();

        var playlistsItemlist = GetChild<ItemList>("PlaylistItemList");
        playlistsItemlist?.Destroy();

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

        AddChild(playlistItemList, "PlaylistItemList");

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Playlist = playlist
            };
        
            playlistItemList.Add(playlistItem);
        }
    }

    public void LoadMusics(Playlist playlist)
    {
        topSection.InPlaylists = false;
        topSection.CurrentPlaylist = playlist;

        GetChild<ItemList>("SongItemList")?.Destroy();
        GetChild<ItemList>("PlaylistItemList")?.Destroy();

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

        AddChild(musicsList, "SongItemList");

        foreach (Song song in playlist.Songs)
        {
            SongItem musicItem = new()
            {
                Playlist = playlist,
                Song = song
            };
        
            musicsList.Add(musicItem);
        }
    }
}