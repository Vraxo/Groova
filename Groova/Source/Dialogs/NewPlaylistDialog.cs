namespace Groova;

public partial class NewPlaylistDialog : Dialog
{
    public override void Start()
    {
        GetNode<Label>("Message").Text = "Enter playlist name:";
        GetNode<TextBox>("TextBox").Confirmed += OnTextBoxConfirmed;
        base.Start();
    }
    
    private void OnTextBoxConfirmed(object? sender, string e)
    {
        var playlistsContainer = GetNode<PlaylistContainer>("/root/PlaylistContainer");

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            if (playlist.Name == e)
            {
                GetNode<Label>("ErrorLabel").Visible = true;
                return;
            }
        }

        playlistsContainer.AddPlaylist(e);
        GetNode<MainScene>("/root").LoadPlaylists();
        Close();
    }
}