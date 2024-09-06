namespace Groova;

public class DeletePlaylistDialog : BaseDeleteDialog
{
    public Playlist Playlist;

    protected override string ItemName => Playlist.Name;

    protected override void OnConfirmButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistsContainer = GetNode<PlaylistContainer>("/root/PlaylistContainer");
        playlistsContainer.RemovePlaylist(Playlist);
        GetNode<MainScene>("/root").LoadPlaylists();

        Close();
    }
}