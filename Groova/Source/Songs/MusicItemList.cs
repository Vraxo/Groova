namespace Groova;

public class MusicItemlist() : ItemList
{
    private PlaylistsContainer playlistsContainer;

    public override void Start()
    {
        playlistsContainer = GetNode<PlaylistsContainer>();
        playlistsContainer.Load();
        base.Start();
    }

    public void Load(Playlist playlist)
    {
        foreach (string path in playlist.Paths)
        {
            MusicItem musicItem = new()
            {
                MusicPath = path
            };

            Add(musicItem);
        }
    }
}