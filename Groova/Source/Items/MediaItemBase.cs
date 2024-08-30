namespace Groova;

public abstract partial class MediaItemBase : Node2D
{
    protected string text;
    protected TexturedRectangle image;
    protected PlaylistsContainer playlistsContainer;

    public override void Start()
    {
        var button = GetChild<Button>();
        button.Text = text;
        button.LeftClicked += OnButtonLeftclicked;
        button.RightClicked += OnButtonRightClicked;

        var imageButton = GetChild<Button>("ImageButton");
        imageButton.LeftClicked += OnImageButtonLeftClicked;
        imageButton.RightClicked += OnImageButtonRightClicked;

        image = GetChild<TexturedRectangle>();
        playlistsContainer = GetNode<PlaylistsContainer>("PlaylistsContainer");
    }

    protected abstract void OnButtonLeftclicked(object? sender, EventArgs e);
    protected abstract void OnButtonRightClicked(object? sender, EventArgs e);

    private void OnImageButtonLeftClicked(object? sender, EventArgs e)
    {
        OpenFileDialog dialog = new();
        dialog.ShowDialog();

        if (dialog.FileName != string.Empty)
        {
            SetImage(dialog.FileName);
            image.Load(dialog.FileName);
        }
    }

    private void OnImageButtonRightClicked(object? sender, EventArgs e)
    {
        SetImage("");
        image.Load("");
    }

    protected abstract void SetImage(string imagePath);
}
