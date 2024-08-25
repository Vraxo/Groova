using Raylib_cs;
using System.ComponentModel;

namespace Sonique;

public class MainNode : Node
{
    public string AudioPath = "Assets/Audio.mp3";

    private MusicPlayer musicPlayer;

    public override void Build()
    {
        AddChild(new TextBox
        {
            Position = new(0, 25),
            Style = new()
            {
                Roundedness = 1
            },
            OnUpdate = (textbox) =>
            {
                float x = Raylib.GetScreenWidth() / 2;
                float y = textbox.Position.Y;

                textbox.Position = new(x, y);
            }
        });

        AddChild(new MusicPlayer
        {
            AutoPlay = false,
            Loop = true,
        });
        
        AddChild(new HorizontalSlider()
        {
            Position = new(50, 0),
            HasButtons = false,
            OnUpdate = (slider) =>
            {
                float y = Raylib.GetScreenHeight() - slider.Size.Y * 4;
                slider.Position = new(slider.Position.X, y);

                float width = Raylib.GetScreenWidth() - 75;
                float height = slider.Size.Y;
                slider.Size = new(width, height);
            }
        }, "AudioSlider");
        
        AddChild(new HorizontalSlider()
        {
            Position = new(50, 0),
            HasButtons = false,
            Percentage = 1,
        }, "VolumeSlider");
        
        AddChild(new Button
        {
            Position = new(25, 20),
            Size = new(32, 32),
            Text = "||",
            OnUpdate = (button) =>
            {
                float x = button.Position.X;
                float y = Raylib.GetScreenHeight() - 40;

                button.Position = new(x, y);
            }
        });

        AddChild(new ItemList
        {
            OnUpdate = (list) =>
            {
                float x = list.Position.X;
                float y = 50;
                list.Position = new(x, y);


                float width = Raylib.GetScreenWidth();
                float height = Raylib.GetScreenHeight() - list.Position.Y - 80;
                list.Size = new(width, height);
            }
        });
    }

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

        //string audioPath = Program.Args.Length > 0 ?
        //                   Program.Args[0] :
        //                   AudioPath;
        //

        if (Program.Args.Length == 0)
        {
            //Environment.Exit(0);
        }
        
        //string audioPath = Program.Args[0];
        
        //musicPlayer = GetChild<MusicPlayer>();
        //musicPlayer.Audio = Raylib.LoadMusicStream(audioPath);
        //musicPlayer.Play();
        
        GetChild<Button>().LeftClicked += OnButtonLeftClicked;
    }

    public override void Update()
    {
        //UpdateAudioSlider();
        UpdateVolumeSlider();
        //UpdateButton();
    }

    private void OnButtonLeftClicked(object? sender, EventArgs e)
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

    private void UpdateAudioSlider()
    {
        var slider = GetChild<HorizontalSlider>("AudioSlider");

        float y = Raylib.GetScreenHeight() * 0.2f;
        slider.Position = new(slider.Position.X, y);

        float width = Raylib.GetScreenWidth() - 75;
        float height = slider.Size.Y;
        slider.Size = new(width, height);
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

    private void UpdateButton()
    {
        var button = GetChild<Button>("Button");

        float x = button.Position.X;
        float y = Raylib.GetScreenHeight() * 0.2f;

        button.Position = new(x, y);
    }
}