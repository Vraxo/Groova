namespace Groova;

public class SongItemList : BaseItemItemList
{
    public Playlist Playlist;

    public override void Start()
    {
        foreach (Song song in Playlist.Songs)
        {
            SongItem musicItem = new()
            {
                Playlist = Playlist,
                Song = song
            };

            Add(musicItem);
        }

        base.Start();
    }
}