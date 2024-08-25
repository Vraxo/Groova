using Raylib_cs;

namespace Groova;

public class Panel : ClickableRectangle
{
    private readonly Color color = new(24, 24, 24, 255);
    private readonly Color outlineColor = new(128, 128, 128, 255);

    public override void Start()
    {
        OriginPreset = OriginPreset.TopLeft;
        Layer = ClickableLayer.Panels;

        base.Start();
    }

    public override void Update()
    {
        Draw();
    }

    private void Draw()
    {
        Raylib.DrawRectangleV(GlobalPosition - Origin, Size, color);

        Raylib.DrawRectangleLines(
            (int)(GlobalPosition.X - Origin.X),
            (int)(GlobalPosition.Y - Origin.Y),
            (int)Size.X,
            (int)Size.Y,
            outlineColor);
    }
}