namespace Groova;

public abstract partial class BaseItem : Node2D
{
    protected string Text;
    protected TexturedRectangle image;
    protected PlaylistContainer playlistsContainer;

    public override void Start()
    {
        var button = GetNode2<Button>("Button");
        button.Text = Text;
        button.LeftClicked += OnButtonLeftClicked;
        button.RightClicked += OnButtonRightClicked;

        var imageButton = GetNode2<Button>("ImageButton");
        imageButton.LeftClicked += OnImageButtonLeftClicked;
        imageButton.RightClicked += OnImageButtonRightClicked;

        image = GetNode2<TexturedRectangle>("Image");
        playlistsContainer = GetNode2<PlaylistContainer>("/root/PlaylistContainer");
    }

    protected abstract void OnButtonLeftClicked(object? sender, EventArgs e);
    protected abstract void OnButtonRightClicked(object? sender, EventArgs e);
    
    protected abstract void SetImage(string imagePath);

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
}