using Raylib_cs;

namespace Groova;

public partial class PlaylistItem : Node2D
{
    public string Text = "";
    public Playlist? Playlist = null;

    public override void Start()
    {
        var button = GetChild<Button>();
        button.Text = Text;
        button.LeftClicked += OnButtonLeftclicked;
        button.RightClicked += OnButtonRightClicked;

        var imageButton = GetChild<Button>("ImageButton");
        imageButton.LeftClicked += OnImageButtonLeftClicked;

        var image = GetChild<TexturedRectangle>();
        image.Texture = Raylib.LoadTexture(Playlist.ImagePath);
    }

    private void OnButtonLeftclicked(object? sender, EventArgs e)
    {
        GetNode<MainScene>("").LoadMusics(Playlist);
    }

    private void OnImageButtonLeftClicked(object? sender, EventArgs e)
    {
        OpenFileDialog dialog = new();
        dialog.ShowDialog();

        if (dialog.FileName != string.Empty)
        {
            var playlistsContainer = GetNode<PlaylistsContainer>("PlaylistsContainer");
            playlistsContainer.SetPlaylistImage(Playlist, dialog.FileName);
            GetChild<TexturedRectangle>().Texture = Raylib.LoadTexture(dialog.FileName);
        }
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