using Raylib_cs;

namespace Groova;

public partial class MainNode : Node
{
    public string AudioPath = "Assets/Audio.mp3";

    private MusicPlayer musicPlayer;

    public override void Ready()
    {
        //var list = GetChild<ItemList>();
        //list.ItemSize = new(list.ItemSize.X, list.ItemSize.Y * 3);
        //
        //list.AddItem(new Label { Text = "Item 1" });
        //list.AddItem(new Label { Text = "Item 2" });
        //list.AddItem(new Label { Text = "Item 3" });
        //list.AddItem(new Label { Text = "Item 4" });
        //list.AddItem(new Label { Text = "Item 5" });
        //list.AddItem(new Label { Text = "Item 6" });
        //list.AddItem(new Label { Text = "Item 7" });
        //list.AddItem(new Label { Text = "Item 8" });
        //list.AddItem(new Label { Text = "Item 9" });
        //list.AddItem(new Label { Text = "Item 10" });
        //list.AddItem(new Label { Text = "Item 11" });
        //list.AddItem(new Label { Text = "Item 12" });

        //musicPlayer = GetChild<MusicPlayer>();
        //musicPlayer.Audio = Raylib.LoadMusicStream(audioPath);
        //musicPlayer.Play();
        
        GetChild<Button>("PlayButton").LeftClicked += OnPlayButtonLeftClicked;
        GetChild<Button>("AddButton").LeftClicked += OnAddButtonLeftClicked;

        Setup();
    }

    public override void Update()
    {
        //UpdateAudioSlider();
        UpdateVolumeSlider();
        //UpdateButton();
    }

    private void OnPlayButtonLeftClicked(object? sender, EventArgs e)
    {
        if (musicPlayer.Playing)
        {
            musicPlayer.Pause();
        }
        else
        {
            musicPlayer.Resume();
        }

        var button = sender as Button;

        button.Text = button.Text == ">" ?
                      "||" :
                      ">";
    }

    private void OnAddButtonLeftClicked(object? sender, EventArgs e)
    {
        Program.RootNode.AddChild(new NewPlaylistDialog());
    }

    private void Setup()
    {
        string playlistsDirectory = "Resources/Playlists";

        if (Directory.Exists(playlistsDirectory))
        {
            string[] playlistFiles = Directory.GetFiles(playlistsDirectory);

            foreach (string playlistFile in playlistFiles)
            {
                Console.WriteLine(playlistFile);
            }
        }
        else
        {
            Directory.CreateDirectory(playlistsDirectory);
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