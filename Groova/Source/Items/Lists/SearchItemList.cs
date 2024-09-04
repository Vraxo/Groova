namespace Groova;

public class SearchItemList : BaseItemItemList
{
    private PlaylistContainer playlistContainer;

    public override void Start()
    {
        base.Start();

        playlistContainer = GetNode<PlaylistContainer>("PlaylistContainer");

        var textBox = GetNode<TextBox>("TopSection/SearchBar");
        textBox.TextChanged += OnSearchBarTextChanged;
        Search(textBox.Text);
    }

    private void OnSearchBarTextChanged(object? sender, string e)
    {
        Search(e.ToLower());
        Console.WriteLine("searching for " + e);
    }

    private void Search(string text)
    {
        Clear();

        foreach (Playlist playlist in playlistContainer.Playlists)
        {
            if (playlist.Name.ToLower().Contains(text))
            {
                PlaylistItem playlistItem = new()
                {
                    Playlist = playlist
                };

                Add(playlistItem);
            }

            foreach (Song song in playlist.Songs)
            {
                string name = Path.GetFileNameWithoutExtension(song.FilePath);

                if (name.ToLower().Contains(text))
                {
                    SongItem songItem = new()
                    {
                        Song = song
                    };

                    Add(songItem);
                }
            }
        }
    }
}