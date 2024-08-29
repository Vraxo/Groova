using Raylib_cs;

namespace Groova;

public partial class PlaylistItem : Node2D
{
    public string Text = "";
    public Playlist? Playlist = null;

    private TexturedRectangle image;
    private PlaylistsContainer playlistsContainer;

    public override void Start()
    {
        var button = GetChild<Button>();
        button.Text = Text;
        button.LeftClicked += OnButtonLeftclicked;
        button.RightClicked += OnButtonRightClicked;

        var imageButton = GetChild<Button>("ImageButton");
        imageButton.LeftClicked += OnImageButtonLeftClicked;
        imageButton.RightClicked += OnImageButtonRightClicked;

        image = GetChild<TexturedRectangle>();
        image.Texture = Raylib.LoadTexture(Playlist.ImagePath);

        playlistsContainer = GetNode<PlaylistsContainer>("PlaylistsContainer");
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
            playlistsContainer.SetPlaylistImage(Playlist, dialog.FileName);
            Console.WriteLine(image is null);
            image.Texture = Raylib.LoadTexture(dialog.FileName);
        }
    }

    private void OnImageButtonRightClicked(object? sender, EventArgs e)
    {
        playlistsContainer.SetPlaylistImage(Playlist, "");
        image.Texture = Raylib.LoadTexture("");
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