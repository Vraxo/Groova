using Raylib_cs;

namespace Groova;

public class SongItemList : ItemList
{
    public Playlist Playlist;

    public SongItemList()
    {
        ItemSize = new(100, 40);
    }

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

    public override void Update()
    {
        float x = Position.X;
        float y = 50;
        Position = new(x, y);

        float width = Raylib.GetScreenWidth();
        float height = Raylib.GetScreenHeight() - Position.Y - 80;
        Size = new(width, height);

        base.Update();
    }
}