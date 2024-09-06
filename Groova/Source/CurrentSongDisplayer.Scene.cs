namespace Groova;

public partial class CurrentSongDisplayer : Node2D
{
    public override void Build()
    {
        AddChild(new TexturedRectangle
        {
            Position = new(24, 24),
        }, "Image");

        AddChild(new Label
        {
            Position = new(8+32+10, 25),
            OnUpdate = (label) =>
            {
                label.AvailableWidth = Window.Width - 210;
            }
        }, "SongName");

        AddChild(new Button
        {
            Position = new(10, 30),
            Text = "Stop",
            OnUpdate = (button) =>
            {
                float x = Window.Width - 60;
                float y = button.Position.Y;
                button.Position = new(x, y);
            }
        }, "ReplayModeButton");
    }
}