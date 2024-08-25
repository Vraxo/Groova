using Raylib_cs;

namespace Groova;

public partial class NewPlaylistDialog : Node2D
{
    public override void Start()
    {
        Origin = GetChild<Panel>().Size / 2;
        GetNode<ClickManager>().MinLayer = ClickableLayer.DialogButtons;
        GetChild<Button>().LeftClicked += OnButtonLeftClicked;
        GetChild<TextBox>().Confirmed += OnTextBoxConfirmed;
    }

    public override void Update()
    {
        UpdatePosition();
        Console.WriteLine(GetNode<ClickManager>().MinLayer);
    }

    private void OnButtonLeftClicked(object? sender, EventArgs e)
    {
        Close();
    }

    private void OnTextBoxConfirmed(object? sender, string e)
    {
        GetNode<PlaylistsContainer>("PlaylistsContainer").AddPlaylist(e);
        Close();
    }

    private void Close()
    {
        GetNode<ClickManager>().MinLayer = 0;
        Destroy();
    }

    private void UpdatePosition()
    {
        float x = Raylib.GetScreenWidth() / 2;
        float y = Raylib.GetScreenHeight() / 2;

        Position = new(x, y);
    }
}