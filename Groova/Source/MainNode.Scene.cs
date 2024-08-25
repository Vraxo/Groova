using Raylib_cs;

namespace Groova;

public partial class MainNode : Node
{
    public override void Build()
    {
        AddChild(new ClickManager());

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
        }, "PlayButton");

        AddChild(new PlaylistsContainer());

        AddChild(new Button
        {
            Position = new(20, 25),
            Size = new(24, 24),
            Text = "+",
        }, "AddButton");

        AddChild(new TextBox
        {
            Position = new(0, 25),
            Style = new()
            {
                Roundness = 1
            },
            OnUpdate = (textBox) =>
            {
                float x = 10 + Raylib.GetScreenWidth() / 2;
                float y = textBox.Position.Y;
                textBox.Position = new(x, y);

                float width = Raylib.GetScreenWidth() * 0.85f;
                float height = textBox.Size.Y;
                textBox.Size = new(width, height);
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
}