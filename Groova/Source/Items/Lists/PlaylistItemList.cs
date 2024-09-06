namespace Groova;

public class PlaylistItemList : BaseItemItemList
{
    public override void Start()
    {
        base.Start();

        var playlistContainer = GetNode<PlaylistContainer>("/root/PlaylistContainer"); 

        foreach (Playlist playlist in playlistContainer.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Playlist = playlist
            };

            Add(playlistItem);
        }
    }
}