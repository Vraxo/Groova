namespace Groova;

public partial class DeleteSongDialog : Dialog
{
    public Playlist Playlist;
    public string SongPath;

    public override void Start()
    {
        GetChild<Button>("DeleteButton").LeftClicked += OnDeleteButtonLeftClicked;
        SetLabelText();
        base.Start();
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

    private static string TruncateSongName(string songName)
    {
        if (songName.Length > 25)
        {
            return songName.Substring(0, 22) + "...";
        }

        return songName;
    }
}