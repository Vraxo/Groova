using Raylib_cs;

namespace Groova;

public partial class MainNode : Node
{
    public MusicPlayer MusicPlayer;

    private PlaylistsContainer playlistsContainer;
    private ItemList playlistsList;
    private Playlist? currentPlaylist = null;
    private bool inPlaylists = true;

    public override void Ready()
    {
        MusicPlayer = GetChild<MusicPlayer>();
        //MusicPlayer.Audio = Raylib.LoadMusicStream(audioPath);
        //MusicPlayer.Play();

        playlistsContainer = GetChild<PlaylistsContainer>();
        playlistsList = GetChild<ItemList>();

        GetChild<Button>("PlayButton").LeftClicked += OnPlayButtonLeftClicked;
        GetChild<Button>("AddButton").LeftClicked += OnAddButtonLeftClicked;

        LoadPlaylists();
    }

    public override void Update()
    {
        //UpdateAudioSlider();
        UpdateVolumeSlider();
        //UpdateButton();
    }

    private void OnPlayButtonLeftClicked(object? sender, EventArgs e)
    {
        if (MusicPlayer.Playing)
        {
            MusicPlayer.Pause();
        }
        else
        {
            MusicPlayer.Resume();
        }

        var button = sender as Button;

        button.Text = button.Text == ">" ?
                      "||" :
                      ">";
    }

    private void OnAddButtonLeftClicked(object? sender, EventArgs e)
    {
        if (inPlaylists)
        {
            AddChild(new NewPlaylistDialog());
        }
        else
        {
            OpenFileDialog dialog = new();
            dialog.ShowDialog();

            if (dialog.FileNames.Length > 0)
            {
                foreach (string name in dialog.FileNames)
                {
                    playlistsContainer.AddMusic(currentPlaylist, name);
                }
            }

            //LoadMusics(currentPlaylist);
        }
    }

    private void OnPlaylistButtonLeftClicked(object? sender, EventArgs e)
    {
        var playlistButton = sender as PlaylistButton;
        Playlist playlist = playlistButton.Playlist;
        LoadMusics(playlist);
    }

    public void LoadPlaylists()
    {
        playlistsList.Activate();
        playlistsList.Clear();
        playlistsContainer.Load();

        foreach (Playlist playlist in playlistsContainer.Playlists)
        {
            PlaylistButton button = new()
            {
                Size = new(100, 40),
                OriginPreset = OriginPreset.TopLeft,
                TextOriginPreset = OriginPreset.CenterLeft,
                TextPadding = new(25, 0),
                Text = playlist.Name,
                Playlist = playlist,
                Style = new()
                {
                    Roundness = 0
                },
                OnUpdate = (button) =>
                {
                    float width = Raylib.GetScreenWidth();
                    float height = button.Size.Y;
                    button.Size = new(width, height);
                },
            };

            playlistsList.Add(button);

            button.LeftClicked += OnPlaylistButtonLeftClicked;
        }
    }

    private void LoadMusics(Playlist playlist)
    {
        inPlaylists = false;
        currentPlaylist = playlist;
        playlistsList.Deactivate();

        ItemList musicList = new()
        {
            ItemSize = new(100, 40),
            OnUpdate = (list) =>
            {
                float x = list.Position.X;
                float y = 50;
                list.Position = new(x, y);

                float width = Raylib.GetScreenWidth();
                float height = Raylib.GetScreenHeight() - list.Position.Y - 80;
                list.Size = new(width, height);
            }
        };

        AddChild(musicList);

        foreach (string path in playlist.Paths)
        {
            MusicItem musicItem = new()
            {
                MusicPath = path
            };

            musicList.Add(musicItem);
        }
    }

    private void UpdateVolumeSlider()
    {
        var slider = GetChild<HorizontalSlider>("VolumeSlider");

        float screenWidth = Raylib.GetScreenWidth();

        var audioSlider = GetChild<HorizontalSlider>("AudioSlider");

        float spaceBetweenAudioSliderAndBorder = screenWidth - audioSlider.Size.X - audioSlider.GlobalPosition.X;

        float x = screenWidth - slider.Size.X - spaceBetweenAudioSliderAndBorder;
        float y = Raylib.GetScreenHeight() * 0.5f;
        slider.Position = new(x, y);

        float width = screenWidth / 5;
        float height = slider.Size.Y;
        slider.Size = new(width, height);
    }
}