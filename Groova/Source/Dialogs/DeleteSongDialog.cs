using Raylib_cs;

namespace Groova;

public partial class DeleteSongDialog : Node2D
{
    public Playlist Playlist;
    public string SongPath;

    public override void Start()
    {
        Origin = GetChild<ColoredRectangle>().Size / 2;

        GetNode<ClickManager>().MinLayer = ClickableLayer.DialogButtons;

        GetChild<Button>("CloseButton").LeftClicked += OnCloseButtonLeftClicked;
        GetChild<Button>("DeleteButton").LeftClicked += OnDeleteButtonLeftClicked;

        SetLabelText();
    }

    public override void Update()
    {
        UpdatePosition();
    }

    private void OnCloseButtonLeftClicked(object? sender, EventArgs e)
    {
        Close();
    }

    private void OnDeleteButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistsContainer = GetNode<PlaylistsContainer>();
        playlistsContainer.RemoveMusic(Playlist, SongPath);
        GetNode<MainScene>("").LoadMusics(Playlist);

        Close();
    }

    private void SetLabelText()
    {
        string songName = Path.GetFileNameWithoutExtension(SongPath);
        string truncatedSongName = TruncateSongName(songName);

        GetChild<Label>().Text = $"Delete '{truncatedSongName}'?";
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

    private static string TruncateSongName(string songName)
    {
        if (songName.Length > 25)
        {
            return songName.Substring(0, 22) + "...";
        }

        return songName;
    }
}