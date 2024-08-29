namespace Groova;

public partial class MusicItem : Node2D
{
    public Playlist Playlist;
    public string SongPath = "";

    private PlaylistsContainer playlistsContainer;

    public override void Start()
    {
        var button = GetChild<Button>();
        button.Text = Path.GetFileNameWithoutExtension(SongPath);
        button.LeftClicked += OnButtonLeftclicked;
        button.RightClicked += OnButtonRightClicked;

        playlistsContainer = GetNode<PlaylistsContainer>();
    }

    private void OnButtonLeftclicked(object? sender, EventArgs e)
    {
        var musicPlayer = GetNode<MusicPlayer>("MusicPlayer");
        musicPlayer.Load(SongPath);
        musicPlayer.Play();
    }

    private void OnButtonRightClicked(object? sender, EventArgs e)
    {
        DeleteSongDialog dialog = new()
        {
            Playlist = Playlist,
            SongPath = SongPath
        };

        RootNode.AddChild(dialog);
    }
}