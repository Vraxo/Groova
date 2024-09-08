namespace Groova;

public class PlaylistItemList : BaseItemItemList
{
    public override void Start()
    {
        foreach (Playlist playlist in PlaylistContainer.Instance.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Playlist = playlist
            };

            Add(playlistItem);
        }

        base.Start();
    }
}