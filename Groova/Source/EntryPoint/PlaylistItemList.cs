using Raylib_cs;

namespace Groova;

public class PlaylistItemList : ItemList
{
    public PlaylistItemList()
    {
        ItemSize = new(100, 40);
    }

    public override void Start()
    {
        base.Start();

        var playlistContainer = GetNode<PlaylistContainer>("PlaylistContainer"); 

        foreach (Playlist playlist in playlistContainer.Playlists)
        {
            PlaylistItem playlistItem = new()
            {
                Playlist = playlist
            };

            Add(playlistItem);
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