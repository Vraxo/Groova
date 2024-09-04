using Raylib_cs;

namespace Groova;

public class ColoredRectangle : ClickableRectangle
{
    public Color FillColor = new(24, 24, 24, 255);
    public Color OutlineColor = new(128, 128, 128, 255);
    public Action<ColoredRectangle> OnUpdate = (rectangle) => { };

    public ColoredRectangle()
    {
        Size = new(32, 32);
        OriginPreset = OriginPreset.TopLeft;
        Layer = ClickableLayer.Panels;
    }

    public override void Update()
    {
        OnUpdate(this);
        Draw();
    }

    private void Draw()
    {
        Raylib.DrawRectangleV(
            GlobalPosition - Origin, 
            Size, 
            FillColor);

        Raylib.DrawRectangleLines(
            (int)(GlobalPosition.X - Origin.X),
            (int)(GlobalPosition.Y - Origin.Y),
            (int)Size.X,
            (int)Size.Y,
            OutlineColor);
    }
}