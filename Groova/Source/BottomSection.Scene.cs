namespace Groova;

public partial class BottomSection : Node2D
{
    public override void Build()
    {
        AddChild(new CurrentSongDisplayer());

        AddChild(new Button
        {
            Position = new(25, 20),
            Size = new(32, 32),
            Text = ">",
            OnUpdate = (button) =>
            {
                float x = button.Position.X;
                float y = Window.Height - 40;

                button.Position = new(x, y);
            }
        }, "PauseOrResumeButton");

        AddChild(new HorizontalSlider
        {
            Position = new(100, 0),
            HasButtons = false,
            OnUpdate = (slider) =>
            {
                float y = Window.Height - slider.Size.Y * 4;
                slider.Position = new(slider.Position.X, y);

                float width = Window.Width - 160;
                float height = slider.Size.Y;
                slider.Size = new(width, height);
            }
        }, "AudioSlider");

        AddChild(new Label
        {
            Position = new(50, 10),
            Text = "01:06",
            OnUpdate = (label) =>
            {
                float x = label.Position.X;
                float y = Window.Height - 40;
                label.Position = new(x, y);
            }
        }, "TimePlayedLabel");

        AddChild(new Label
        {
            Position = new(100, 10),
            Text = "01:06",
            OnUpdate = (label) =>
            {
                float x = Window.Width - label.Size.X * 1.3f;
                float y = Window.Height - 40;
                label.Position = new(x, y);
            }
        }, "TotalTimeLabel");

        AddChild(new HorizontalSlider
        {
            Position = new(50, 0),
            HasButtons = false,
            InitialPercentage = 1f,
            DefaultPercentage = 1f,
            OnUpdate = (slider) =>
            {
                //var label = GetNode<Label>("TotalTimeLabel");
                //
                //float spaceBetweenLabelAndBorder = Window.Width - label.Size.X - label.GlobalPosition.X;
                //
                //float x = Window.Width - slider.Size.X - spaceBetweenLabelAndBorder;
                //float y = Window.Height - 15;
                //slider.Position = new(x, y);
                //
                //float width = Window.Width - slider.Position.X - spaceBetweenLabelAndBorder;
                //float height = slider.Size.Y;
                //slider.Size = new(width, height);

                var label = GetNode<Label>("TotalTimeLabel");
                var audioSlider = GetNode<HorizontalSlider>("AudioSlider");

                float spaceBetweenLabelAndBorder = Window.Width - label.Size.X - label.GlobalPosition.X;

                float x = audioSlider.Position.X + audioSlider.Size.X / 2 + 10;
                
                float y = Window.Height - 15;
                slider.Position = new(x, y);

                float width = Window.Width - slider.Position.X - spaceBetweenLabelAndBorder;
                float height = slider.Size.Y;
                slider.Size = new(width, height);
            }
        }, "VolumeSlider");

        AddChild(new HorizontalSlider
        {
            Position = new(50, 0),
            HasButtons = false,
            InitialPercentage = 0.5f,
            DefaultPercentage = 0.5f,
            OnUpdate = (slider) =>
            {
                var label = GetNode<Label>("TimePlayedLabel");
                var audioSlider = GetNode<HorizontalSlider>("AudioSlider");

                float x = slider.Position.X;
                float y = Window.Height - 15;
                slider.Position = new(x, y);

                float width = label.Size.X + audioSlider.Size.X / 2;
                float height = slider.Size.Y;
                slider.Size = new(width, height);
            }
        }, "PitchSlider");
    }
}