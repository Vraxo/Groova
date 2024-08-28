using Raylib_cs;

namespace Groova;

public class TextBoxShape : Node2D
{
    public ButtonStyle Style = new();

    private TextBox parent;

    public override void Ready()
    {
        parent = GetParent<TextBox>();
    }

    public override void Update()
    {
        DrawShape();
        base.Update();
    }

    private void DrawShape()
    {
        if (!Visible)
        {
            return;
        }

        DrawOutline();
        DrawInside();
    }

    private void DrawInside()
    {
        Rectangle rectangle = new()
        {
            Position = parent.GlobalPosition - parent.Origin,
            Size = parent.Size
        };

        Raylib.DrawRectangleRounded(
            rectangle,
            parent.Style.Current.Roundness,
            (int)Size.Y,
            parent.Style.Current.FillColor);
    }

    private void DrawOutline()
    {
        if (parent.Style.Current.OutlineThickness < 0)
        {
            return;
        }

        for (int i = 0; i <= parent.Style.Current.OutlineThickness; i++)
        {
            Rectangle rectangle = new()
            {
                Position = parent.GlobalPosition - parent.Origin - new Vector2(i, i),
                Size = new(parent.Size.X + i + 1, parent.Size.Y + i + 1)
            };

            Raylib.DrawRectangleRounded(
                rectangle,
                parent.Style.Current.Roundness,
                (int)Size.Y,
                parent.Style.Current.OutlineColor);
        }
    }

    //private void DrawShape()
    //{
    //    Rectangle rectangle = new()
    //    {
    //        Position = parent.GlobalPosition - parent.Origin,
    //        Size = parent.Size
    //    };
    //
    //    DrawRectangle(rectangle);
    //    DrawOutline(rectangle);
    //}
    //
    //private void DrawRectangle(Rectangle rectangle)
    //{
    //    Raylib.DrawRectangleRounded(
    //        rectangle,
    //        parent.Style.Current.Roundedness,
    //        (int)parent.Size.Y,
    //        parent.Style.Current.FillColor);
    //}
    //
    //private void DrawOutline(Rectangle rectangle)
    //{
    //    if (parent.Style.Current.OutlineThickness > 0)
    //    {
    //        Raylib.DrawRectangleRoundedLines(
    //            rectangle,
    //            parent.Style.Current.Roundedness,
    //            (int)Size.Y,
    //            parent.Style.Current.OutlineThickness,
    //            parent.Style.Current.OutlineColor);
    //    }
    //}
}