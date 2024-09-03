using System.Drawing.Imaging;
using System.Xml.Linq;

namespace Groova;

public partial class TopSection : Node2D
{
    public bool InPlaylists = true;
    public Playlist? CurrentPlaylist = null;

    private MainScene parent;
    private PlaylistContainer playlistsContainer;

    public override void Start()
    {
        parent = Parent as MainScene;
        playlistsContainer = GetNode<PlaylistContainer>();

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
                playlistsContainer.AddSong(CurrentPlaylist, name);
            }
        }

        parent.LoadMusics(CurrentPlaylist);
    }

    private bool IsFileValid(string path)
    {
        bool original = true;

        foreach (Song song in CurrentPlaylist.Songs)
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
        if (!InPlaylists)
        {
            parent.LoadPlaylists();
        }
    }
}