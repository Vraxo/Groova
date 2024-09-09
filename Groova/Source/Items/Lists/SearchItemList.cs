namespace Groova;

public class SearchItemList : BaseItemItemList
{
    public override void Start()
    {
        var textBox = GetNode<TextBox>("/root/TopSection/SearchBar");
        textBox.TextChanged += OnSearchBarTextChanged;
        textBox.Cleared += OnSearchBarCleared;

        Search(textBox.Text);

        base.Start();
    }

    private void OnSearchBarCleared(object? sender, EventArgs e)
    {
        GetNode<MainScene>("/root").StopSearch();
    }

    private void OnSearchBarTextChanged(object? sender, string e)
    {
        if (e.Length > 0)
        {
            Search(e.ToLower());
        }
    }

    private void Search(string text)
    {
        Clear();

        foreach (Playlist playlist in PlaylistContainer.Instance.Playlists)
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
                        Song = song,
                        Playlist = playlist
                    };

                    Add(songItem);
                }
            }
        }
    }
}