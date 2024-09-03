namespace Groova;

public partial class TopSection : Node2D
{
    private MainScene mainScene;
    private PlaylistContainer playlistsContainer;

    public override void Start()
    {
        mainScene = Parent as MainScene;
        playlistsContainer = GetNode<PlaylistContainer>();

        GetChild<Button>("AddButton").LeftClicked += OnAddButtonLeftClicked;
        GetChild<Button>("ReturnButton").LeftClicked += OnReturnButtonLeftClicked;
        GetChild<TextBox>("SearchBar").FirstCharacterEntered += OnSearchBarFirstCharacterEntered;
        GetChild<TextBox>("SearchBar").Cleared += OnSearchBarCleared;
    }

    private void OnAddButtonLeftClicked(object? sender, EventArgs e)
    {
        AddPlaylistOrMusic();
    }

    private void OnReturnButtonLeftClicked(object? sender, EventArgs e)
    {
        Return();
    }

    private void OnSearchBarFirstCharacterEntered(object? sender, EventArgs e)
    {
        mainScene.Search();
    }

    private void OnSearchBarCleared(object? sender, EventArgs e)
    {
        mainScene.StopSearch();
    }

    private void AddPlaylistOrMusic()
    {
        if (mainScene.InPlaylists)
        {
            CreateNewPlaylistDialog();
        }
        else
        {
            CreateNewSongDialog();
        }
    }

    private void CreateNewPlaylistDialog()
    {
        NewPlaylistDialog dialog = new();
        RootNode.AddChild(dialog);
    }

    private void CreateNewSongDialog()
    {
        OpenFileDialog dialog = new()
        {
            Multiselect = true
        }
;
        dialog.ShowDialog();

        if (dialog.FileNames.Length == 0)
        {
            return;
        }

        foreach (string name in dialog.FileNames)
        {
            if (IsFileValid(name))
            {
                playlistsContainer.AddSong(mainScene.CurrentPlaylist, name);
            }
        }

        mainScene.LoadSongs(mainScene.CurrentPlaylist);
    }

    private bool IsFileValid(string path)
    {
        bool original = true;

        foreach (Song song in mainScene.CurrentPlaylist.Songs)
        {
            if (song.Path == path)
            {
                original = false;
            }
        }

        bool supported = Path.GetExtension(path) == ".mp3";

        return original && supported;
    }

    private void Return()
    {
        if (mainScene.Searching)
        {
            mainScene.StopSearch();
        }
        else if (!mainScene.InPlaylists)
        {
            mainScene.LoadPlaylists();
        }
    }
}