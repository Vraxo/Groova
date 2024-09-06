namespace Groova;

public class DeleteSongDialog : ConfirmDialog
{
    public Playlist Playlist;
    public Song Song;

    public override void Start()
    {
        SetLabelText();
        base.Start();
    }

    protected override void OnConfirmButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistsContainer = GetNode2<PlaylistContainer>("/root/PlaylistContainer");
        playlistsContainer.RemoveSong(Playlist, Song);
        GetNode2<MainScene>("/root").LoadSongs(Playlist);

        Close();
    }

    private void SetLabelText()
    {
        string songName = Path.GetFileNameWithoutExtension(Song.FilePath);
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