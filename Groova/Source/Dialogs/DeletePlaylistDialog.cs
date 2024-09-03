namespace Groova;

public class DeletePlaylistDialog : ConfirmDialog
{
    public Playlist Playlist;

    public override void Start()
    {
        SetLabelText();
        base.Start();
    }

    protected override void OnConfirmButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistsContainer = GetNode<PlaylistContainer>();
        playlistsContainer.RemovePlaylist(Playlist);
        GetNode<MainScene>("").LoadPlaylists(); ;

        Close();
    }

    private void SetLabelText()
    {
        string songName = Path.GetFileNameWithoutExtension(Playlist.Name);
        string truncatedSongName = TruncateSongName(songName);

        GetChild<Label>().Text = $"Delete '{truncatedSongName}'?";
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