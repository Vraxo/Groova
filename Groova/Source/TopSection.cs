namespace Groova;

public partial class TopSection : Node2D
{
    public bool InPlaylists = true;

    private MainScene parent;

    public override void Start()
    {
        parent = Parent as MainScene;

        GetChild<Button>("AddButton").LeftClicked += OnAddButtonLeftClicked;
        GetChild<Button>("ReturnButton").LeftClicked += OnReturnButtonLeftClicked;
    }

    private void OnAddButtonLeftClicked(object? sender, EventArgs e)
    {
        AddPlaylistOrMusic();
    }

    private void OnReturnButtonLeftClicked(object? sender, EventArgs e)
    {
        Return();
    }

    private void AddPlaylistOrMusic()
    {
        if (InPlaylists)
        {
            CreateNewPlaylistDialog();
        }
        else
        {
            CreateNewMusicDialog();
        }
    }

    private void CreateNewPlaylistDialog()
    {
        RootNode.AddChild(new NewPlaylistDialog());
    }

    private void CreateNewMusicDialog()
    {
        OpenFileDialog dialog = new()
        {
            Multiselect = true
        }
;
        dialog.ShowDialog();

        if (dialog.FileNames.Length > 0)
        {
            foreach (string name in dialog.FileNames)
            {
                //playlistsContainer.AddMusic(currentPlaylist, name);
            }
        }

        //parent.LoadMusics(currentPlaylist);
    }

    private void Return()
    {
        if (!InPlaylists)
        {
            //parent.LoadPlaylists();
        }
    }
}