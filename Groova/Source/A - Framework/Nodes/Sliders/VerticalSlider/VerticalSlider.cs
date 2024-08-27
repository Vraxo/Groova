using Raylib_cs;

namespace Groova;

public partial class VerticalSlider : BaseSlider
{
    public VerticalSlider()
    {
        Size = new(10, 100);
        OriginPreset = OriginPreset.TopCenter;
    }

    protected override void UpdatePercentage()
    {
        float currentPosition = Grabber.GlobalPosition.Y;
        float minPos = GlobalPosition.Y - Origin.Y;
        float maxPos = minPos + Size.Y;

        // Calculate and clamp the percentage
        Percentage = Math.Clamp((currentPosition - minPos) / (maxPos - minPos), 0, 1);
    }

    public override void MoveGrabber(int direction)
    {
        if (MaxExternalValue == 0)
        {
            return;
        }

        float x = Grabber.GlobalPosition.X;
        float movementUnit = Size.Y / MathF.Abs(MaxExternalValue);
        float y = Grabber.GlobalPosition.Y + direction * movementUnit;

        Grabber.GlobalPosition = new(x, y);
        UpdatePercentageBasedOnGrabber();
    }

    protected override void HandleClicks()
    {
        if (IsMouseOver())
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && OnTopLeft)
            {
                float x = Grabber.GlobalPosition.X;
                float y = Raylib.GetMousePosition().Y;

                Grabber.GlobalPosition = new(x, y);
                Grabber.Pressed = true;

                OnTopLeft = false;
            }
        }
    }

    protected override void Draw()
    {
        Vector2 position = GlobalPosition - Origin;

        Rectangle emptyRectangle = new()
        {
            Position = position,
            Size = Size
        };

        DrawOutline(emptyRectangle, EmptyStyle.Current);

        Raylib.DrawRectangleRounded(
            emptyRectangle,
            EmptyStyle.Current.Roundness,
            (int)Size.X,
            EmptyStyle.Current.FillColor);

        Rectangle filledRectangle = new()
        {
            Position = new(position.X, position.Y + (1 - Percentage) * Size.Y),
            Size = new(Size.X, Percentage * Size.Y)
        };

        DrawOutline(filledRectangle, FilledStyle.Current);

        Raylib.DrawRectangleRounded(
            filledRectangle,
            FilledStyle.Current.Roundness,
            (int)Size.X,
            FilledStyle.Current.FillColor);
    }

    private void DrawOutline(Rectangle rectangle, ButtonStateStyle style)
    {
        if (style.OutlineThickness <= 0)
        {
            return;
        }

        for (int i = 0; i <= style.OutlineThickness; i++)
        {
            Rectangle outlineRectangle = new()
            {
                Position = rectangle.Position - new Vector2(i, i),
                Size = new(rectangle.Size.X + i + 1, rectangle.Size.Y + i + 1)
            };

            Raylib.DrawRectangleRounded(
                outlineRectangle,
                style.Roundness,
                (int)rectangle.Size.X,
                style.OutlineColor);
        }
    }

    protected override void MoveGrabberTo(float percentage)
    {

    }
}