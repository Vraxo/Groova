namespace Groova;

public class DeleteSongDialog : BaseDeleteDialog
{
    public Playlist Playlist;
    public Song Song;

    protected override string ItemName => Song.FilePath;

    protected override void OnConfirmButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistsContainer = GetNode<PlaylistContainer>("/root/PlaylistContainer");
        playlistsContainer.RemoveSong(Playlist, Song);
        GetNode<MainScene>("/root").LoadSongs(Playlist);

        Close();
    }
}