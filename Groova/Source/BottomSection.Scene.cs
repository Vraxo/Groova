using Raylib_cs;

namespace Groova;

public partial class BottomSection : Node2D
{
    public override void Build()
    {
        AddChild(new Button
        {
            Position = new(20, 20),
            Size = new(32, 32),
            Text = "||",
            OnUpdate = (button) =>
            {
                float x = button.Position.X;
                float y = Raylib.GetScreenHeight() - 40;

                button.Position = new(x, y);
            }
        }, "PauseOrResumeButton");

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
            InitialPercentage = 1f,
            OnUpdate = (slider) =>
            {
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
        }, "VolumeSlider");

        AddChild(new HorizontalSlider()
        {
            Position = new(50, 0),
            HasButtons = false,
            InitialPercentage = 0.5f,
            OnUpdate = (slider) =>
            {
                float screenWidth = Raylib.GetScreenWidth();

                var audioSlider = GetChild<HorizontalSlider>("AudioSlider");

                float spaceBetweenAudioSliderAndBorder = screenWidth - audioSlider.Size.X - audioSlider.GlobalPosition.X;

                float x = slider.Position.X;
                float y = Raylib.GetScreenHeight() - 15;
                slider.Position = new(x, y);

                float width = screenWidth / 3;
                float height = slider.Size.Y;
                slider.Size = new(width, height);
            }
        }, "PitchSlider");
    }
}