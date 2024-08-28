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
    }
}