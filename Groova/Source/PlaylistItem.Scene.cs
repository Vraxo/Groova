using Raylib_cs;

namespace Groova;

public partial class PlaylistItem : Node2D
{
    public override void Build()
    {
        AddChild(new Button
        {
            Size = new(100, 40),
            OriginPreset = OriginPreset.TopLeft,
            TextOriginPreset = OriginPreset.CenterLeft,
            TextPadding = new(50, 0),
            Style = new()
            {
                Roundness = 0
            },
            OnUpdate = (button) =>
            {
                float width = Raylib.GetScreenWidth() - 35;
                float height = button.Size.Y;
                button.Size = new(width, height);
            }
        });

        AddChild(new Button
        {
            Position = new(20, 20),
            Size = new(24, 24),
            Style = new()
            {
                Roundness = 0,
                FillColor = Color.RayWhite
            }
        }, "ImageButton");

        AddChild(new TexturedRectangle
        {
            Position = new(20, 20),
            Size = new(24, 24)
        });
    }
}