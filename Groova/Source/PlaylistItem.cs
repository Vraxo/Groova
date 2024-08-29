namespace Groova;

public partial class PlaylistItem : Node2D
{
    public string Text = "";
    public Playlist? Playlist = null;

    private Button button;

    public override void Start()
    {
        button = GetChild<Button>();
        button.Text = Text;
        button.LeftClicked += OnButtonLeftclicked;
        button.RightClicked += OnButtonRightClicked;
    }

    private void OnButtonLeftclicked(object? sender, EventArgs e)
    {
        GetNode<MainScene>("").LoadMusics(Playlist);
    }

    private void OnButtonRightClicked(object? sender, EventArgs e)
    {
        DeletePlaylistDialog dialog = new()
        {
            Playlist = Playlist
        };

        RootNode.AddChild(dialog);
    }
}