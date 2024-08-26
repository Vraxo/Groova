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
    }

    private void OnButtonLeftclicked(object? sender, EventArgs e)
    {
        GetNode<MainNode>("").LoadMusics(Playlist);
    }
}