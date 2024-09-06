namespace Groova;

public partial class NewPlaylistDialog : Dialog
{
    public override void Start()
    {
        GetNode2<Label>().Text = "Enter playlist name:";
        GetNode2<TextBox>().Confirmed += OnTextBoxConfirmed;
        base.Start();
    }
    
    private void OnTextBoxConfirmed(object? sender, string e)
    {
        var playlistsContainer = GetNode2<PlaylistContainer>("/root/PlaylistContainer");

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            if (playlist.Name == e)
            {
                GetNode<Label>("ErrorLabel").Visible = true;
                return;
            }
        }

        playlistsContainer.AddPlaylist(e);
        GetNode<MainScene>("").LoadPlaylists();
        Close();
    }
}