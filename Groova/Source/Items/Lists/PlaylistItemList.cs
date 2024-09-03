﻿namespace Groova;

public class PlaylistItemList : BaseItemList
{
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
}