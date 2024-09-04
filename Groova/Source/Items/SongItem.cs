﻿namespace Groova;

public partial class SongItem : BaseItem
{
    public Playlist Playlist;
    public Song Song;

    public override void Start()
    {
        Text = Path.GetFileNameWithoutExtension(Song.FilePath);
        base.Start();
        image.Load(Song.ImagePath);
    }

    protected override void OnButtonLeftClicked(object? sender, EventArgs e)
    {
        var songPlayer = GetNode<SongPlayer>("SongPlayer");
        songPlayer.Song = Song;
        songPlayer.Playlist = Playlist;
        songPlayer.Load(Song.FilePath);
        songPlayer.Play();

        var currentSongDisplayer = GetNode<CurrentSongDisplayer>("BottomSection/CurrentSongDisplayer");
        currentSongDisplayer.SetSong(Song);
    }

    protected override void OnButtonRightClicked(object? sender, EventArgs e)
    {
        DeleteSongDialog dialog = new()
        {
            Playlist = Playlist,
            Song = Song
        };

        RootNode.AddChild(dialog);
    }

    protected override void SetImage(string imagePath)
    {
        playlistsContainer.SetSongImage(Song, imagePath);
    }
}