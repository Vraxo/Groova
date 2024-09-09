namespace Groova;

public partial class TopSection : Node2D
{
    private MainScene mainScene;

    public override void Start()
    {
        mainScene = GetParent<MainScene>();

        GetNode<Button>("AddButton").LeftClicked += OnAddButtonLeftClicked;
        GetNode<Button>("ReturnButton").LeftClicked += OnReturnButtonLeftClicked;
        GetNode<TextBox>("SearchBar").FirstCharacterEntered += OnSearchBarFirstCharacterEntered;
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
        mainScene.StartSearch();
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
                PlaylistContainer.Instance.AddSong(mainScene.CurrentPlaylist, name);
            }
        }

        mainScene.LoadSongs(mainScene.CurrentPlaylist);
    }

    private bool IsFileValid(string path)
    {
        bool original = true;

        foreach (Song song in mainScene.CurrentPlaylist.Songs)
        {
            if (song.FilePath == path)
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