using Raylib_cs;

namespace Groova;

public class TextBoxShape : Node2D
{
    public ButtonStyle Style = new();

    private TextBox parent;

    public override void Start()
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
        if (!parent.Visible)
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
            (int)parent.Size.Y,
            parent.Style.Current.FillColor);
    }

    private void DrawOutline()
    {
        if (parent.Style.Current.OutlineThickness < 0)
        {
            return;
        }

        for (int i = 0; i <= Style.Current.OutlineThickness; i++)
        {
            Rectangle rectangle = new()
            {
                Position = GlobalPosition - Origin - new Vector2(i, i),
                Size = new(Size.X + i + 1, Size.Y + i + 1)
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
    //        Position = mainScene.GlobalPosition - mainScene.Origin,
    //        Size = mainScene.Size
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
    //        mainScene.Style.Current.Roundedness,
    //        (int)mainScene.Size.Y,
    //        mainScene.Style.Current.FillColor);
    //}
    //
    //private void DrawOutline(Rectangle rectangle)
    //{
    //    if (mainScene.Style.Current.OutlineThickness > 0)
    //    {
    //        Raylib.DrawRectangleRoundedLines(
    //            rectangle,
    //            mainScene.Style.Current.Roundedness,
    //            (int)Size.Y,
    //            mainScene.Style.Current.OutlineThickness,
    //            mainScene.Style.Current.OutlineColor);
    //    }
    //}
}