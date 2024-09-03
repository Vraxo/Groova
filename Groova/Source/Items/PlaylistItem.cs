namespace Groova;

public partial class PlaylistItem : BaseItem
{
    public Playlist Playlist;

    public override void Start()
    {
        Text = Playlist.Name;
        base.Start();
        image.Load(Playlist.ImagePath);
    }

    protected override void OnButtonLeftClicked(object? sender, EventArgs e)
    {
        GetNode<MainScene>("").LoadMusics(Playlist);
    }

    protected override void OnButtonRightClicked(object? sender, EventArgs e)
    {
        DeletePlaylistDialog dialog = new()
        {
            Playlist = Playlist
        };

        RootNode.AddChild(dialog);
    }

    protected override void SetImage(string imagePath)
    {
        playlistsContainer.SetPlaylistImage(Playlist, imagePath);
    }
}