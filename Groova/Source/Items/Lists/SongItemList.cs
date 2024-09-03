namespace Groova;

public class SongItemList : BaseItemList
{
    public Playlist Playlist;

    public override void Start()
    {
        base.Start();

        foreach (Song song in Playlist.Songs)
        {
            SongItem musicItem = new()
            {
                Playlist = Playlist,
                Song = song
            };

            Add(musicItem);
        }
    }
}