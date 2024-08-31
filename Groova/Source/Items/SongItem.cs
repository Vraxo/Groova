﻿namespace Groova;

public partial class SongItem : MediaItemBase
{
    public Playlist Playlist;
    public Song Song;

    public override void Start()
    {
        text = Path.GetFileNameWithoutExtension(Song.Path);
        base.Start();
        image.Load(Song.ImagePath);
    }

    protected override void OnButtonLeftclicked(object? sender, EventArgs e)
    {
        var musicPlayer = GetNode<SongPlayer>("SongPlayer");
        musicPlayer.Load(Song.Path);
        musicPlayer.Play();
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