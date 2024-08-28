using Raylib_cs;

namespace Groova;

public partial class NewPlaylistDialog : Node2D
{
    public override void Start()
    {
        Origin = GetChild<ColoredRectangle>().Size / 2;
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
        string playlistName = e;
        var playlistsContainer = GetNode<PlaylistsContainer>("PlaylistsContainer");

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            if (playlist.Name == playlistName)
            {
                GetChild<Label>("ErrorLabel").Visible = true;
                return;
            }
        }

        playlistsContainer.AddPlaylist(e);
        Close();
    }

    private void Close()
    {
        GetNode<PlaylistItemlist>("PlaylistsList").Load();
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