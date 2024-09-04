using Raylib_cs;

namespace Groova;

public partial class CurrentSongDisplayer : Node2D
{
    public override void Build()
    {
        AddChild(new TexturedRectangle
        {
            Position = new(24, 24),
        });

        AddChild(new Label
        {
            Position = new(8+32+10, 25),
        });
    }
}