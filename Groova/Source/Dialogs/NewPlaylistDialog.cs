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
        foreach (Playlist playlist in PlaylistContainer.Instance.Playlists)
        {
            if (playlist.Name == e)
            {
                GetNode<Label>("ErrorLabel").Visible = true;
                return;
            }
        }

        PlaylistContainer.Instance.AddPlaylist(e);
        GetNode<MainScene>("/root").LoadPlaylists();
        Close();
    }
}