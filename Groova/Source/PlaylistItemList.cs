namespace Groova;

public class PlaylistItemlist() : ItemList
{
    private PlaylistsContainer playlistsContainer;

    public override void Start()
    {
        playlistsContainer = GetNode<PlaylistsContainer>();
        playlistsContainer.Load();

        base.Start();
    }

    public void Load()
    {
        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Text = playlist.Name,
                Playlist = playlist
            };

            Add(playlistItem);

            Console.WriteLine("added");
        }
    }
}