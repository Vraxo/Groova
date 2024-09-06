namespace Groova;

public class DeletePlaylistDialog : BaseDeleteDialog
{
    public Playlist Playlist;

    protected override string ItemName => Playlist.Name;

    protected override void OnConfirmButtonLeftClicked(object? sender, EventArgs e)
    {
        PlaylistContainer.Instance.RemovePlaylist(Playlist);
        GetNode<MainScene>("/root").LoadPlaylists();

        Close();
    }
}