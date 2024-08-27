using Raylib_cs;

namespace Groova;

public partial class MainNode : Node
{
    public MusicPlayer MusicPlayer;

    private PlaylistsContainer playlistsContainer;
    private ItemList playlistsList;
    private ItemList musicsList;
    private Playlist? currentPlaylist = null;
    private bool inPlaylists = true;

    public override void Start()
    {
        MusicPlayer = GetChild<MusicPlayer>();

        playlistsContainer = GetChild<PlaylistsContainer>();
        playlistsList = GetChild<ItemList>("PlaylistsList");
        
        musicsList = GetChild<ItemList>("MusicsList");
        musicsList.Deactivate();

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
                    playlistsContainer.AddMusic(currentPlaylist, name);
                }
            }

            LoadMusics(currentPlaylist);
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
            PlaylistItem playlistItem = new()
            {
                Text = playlist.Name,
                Playlist = playlist
            };

            playlistsList.Add(playlistItem);
        }
    }

    public void LoadMusics(Playlist playlist)
    {
        inPlaylists = false;
        currentPlaylist = playlist;
        playlistsList.Deactivate();
        musicsList.Activate();
        musicsList.Clear();

        foreach (string path in playlist.Paths)
        {
            MusicItem musicItem = new()
            {
                MusicPath = path
            };

            musicsList.Add(musicItem);
        }
    }

    private void UpdateVolumeSlider()
    {
        var slider = GetChild<HorizontalSlider>("VolumeSlider");

        float screenWidth = Raylib.GetScreenWidth();

        var audioSlider = GetChild<HorizontalSlider>("AudioSlider");

        float spaceBetweenAudioSliderAndBorder = screenWidth - audioSlider.Size.X - audioSlider.GlobalPosition.X;

        float x = screenWidth - slider.Size.X - spaceBetweenAudioSliderAndBorder;
        float y = Raylib.GetScreenHeight() - 15;
        slider.Position = new(x, y);

        float width = screenWidth / 5;
        float height = slider.Size.Y;
        slider.Size = new(width, height);
    }
}