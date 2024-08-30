﻿namespace Groova;

public partial class TopSection : Node2D
{
    public bool InPlaylists = true;
    public Playlist? CurrentPlaylist = null;

    private MainScene parent;
    private PlaylistsContainer playlistsContainer;

    public override void Start()
    {
        parent = Parent as MainScene;
        playlistsContainer = GetNode<PlaylistsContainer>();

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

        if (dialog.FileNames.Length > 0)
        {
            foreach (string name in dialog.FileNames)
            {
                bool valid = true;

                foreach (Song song in CurrentPlaylist.Songs)
                {
                    if (song.Path == name || Path.GetExtension(name) != ".mp3")
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    playlistsContainer.AddSong(CurrentPlaylist, name);
                }
            }
        }

        parent.LoadMusics(CurrentPlaylist);
    }

    private void Return()
    {
        if (!InPlaylists)
        {
            parent.LoadPlaylists();
        }
    }
}