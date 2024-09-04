namespace Groova;

public partial class NewPlaylistDialog : Dialog
{
    public override void Start()
    {
        GetChild<Label>().Text = "Enter playlist name:";
        GetChild<TextBox>().Confirmed += OnTextBoxConfirmed;
        base.Start();
    }
    
    private void OnTextBoxConfirmed(object? sender, string e)
    {
        var playlistsContainer = GetNode<PlaylistContainer>("PlaylistContainer");

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            if (playlist.Name == e)
            {
                GetChild<Label>("ErrorLabel").Visible = true;
                return;
            }
        }

        playlistsContainer.AddPlaylist(e);
        GetNode<MainScene>("").LoadPlaylists();
        Close();
    }
}