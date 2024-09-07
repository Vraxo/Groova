using Raylib_cs;

namespace Groova;

public class TextBoxShape : Node2D
{
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

    //private void Draw()
    //{
    //    if (!(Visible && readyForVisibility))
    //    {
    //        Console.WriteLine("returning");
    //        return;
    //    }
    //    DrawShape();
    //}
    //
    //private void DrawShape()
    //{
    //    DrawOutline();
    //    DrawInside();
    //}
    //
    //private void DrawInside()
    //{
    //    Rectangle rectangle = new()
    //    {
    //        Position = parent.GlobalPosition - parent.Origin,
    //        Size = parent.Size
    //    };
    //
    //    Raylib.DrawRectangleRounded(
    //        rectangle,
    //        parent.Style.Current.Roundness,
    //        (int)parent.Size.Y,
    //        parent.Style.Current.FillColor);
    //}
    //
    //private void DrawOutline()
    //{
    //    if (parent.Style.Current.OutlineThickness <= 0)
    //    {
    //        return;
    //    }
    //
    //    Vector2 position = parent.GlobalPosition - parent.Origin;
    //
    //    Rectangle rectangle = new()
    //    {
    //        Position = position,
    //        Size = parent.Size
    //    };
    //
    //    for (int i = 0; i <= parent.Style.Current.OutlineThickness; i++)
    //    {
    //        Rectangle outlineRectangle = new()
    //        {
    //            Position = rectangle.Position - new Vector2(i, i),
    //            Size = new(rectangle.Size.X + i + 1, rectangle.Size.Y + i + 1)
    //        };
    //
    //        Raylib.DrawRectangleRounded(
    //            outlineRectangle,
    //            parent.Style.Current.Roundness,
    //            (int)rectangle.Size.X,
    //            parent.Style.Current.OutlineColor);
    //    }
    //}

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
        if (parent.Style.Current.OutlineThickness <= 0)
        {
            return;
        }

        for (int i = 1; i <= parent.Style.Current.OutlineThickness; i++)
        {
            // Adjust position to move inward by half the current outline thickness
            Vector2 offset = new Vector2(i / 2f, i / 2f);

            Rectangle rectangle = new()
            {
                Position = parent.GlobalPosition - parent.Origin - offset, // Adjust position
                Size = new(parent.Size.X + i, parent.Size.Y + i) // Increase size equally in both dimensions
            };

            Raylib.DrawRectangleRounded(
                rectangle,
                parent.Style.Current.Roundness,
                (int)parent.Size.Y,
                parent.Style.Current.OutlineColor);
        }
    }
}