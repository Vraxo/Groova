using Raylib_cs;

namespace Groova;

public class Button : ClickableRectangle
{
    public string Text { get; set; } = "";
    public Vector2 TextPadding { get; set; } = Vector2.Zero;
    public Vector2 TextOrigin { get; set; } = Vector2.Zero;
    public OriginPreset TextOriginPreset { get; set; } = OriginPreset.Center;
    public ButtonStyle Style { get; set; } = new();
    public bool PressedLeft { get; set; } = false;
    public bool PressedRight { get; set; } = false;
    public ButtonClickMode LeftClickMode { get; set; } = ButtonClickMode.Limitless;
    public ButtonClickMode RightClickMode { get; set; } = ButtonClickMode.Limitless;
    public Action<Button> OnUpdate = (button) => { };
    public event EventHandler? LeftClicked;
    public event EventHandler? RightClicked;

    private bool alreadyClicked = false;

    public Button()
    {
        Size = new(100, 26);
    }

    public override void Update()
    {
        UpdateTextOrigin();
        Draw();
        HandleInput();
        OnUpdate(this);
        base.Update();
    }

    private void HandleInput()
    {
        HandleLeftClicks();
        HandleRightClicks();
    }

    private void UpdateTextOrigin()
    {
        TextOrigin = TextOriginPreset switch
        {
            OriginPreset.Center => Size / 2,
            OriginPreset.CenterLeft => new(0, Size.Y / 2),
            OriginPreset.CenterRight => new(Size.X, Size.Y / 2),
            OriginPreset.TopLeft => new(0, 0),
            OriginPreset.TopRight => new(Size.X, 0),
            OriginPreset.TopCenter => new(Size.X / 2, 0),
            OriginPreset.BottomLeft => new(0, Size.Y),
            OriginPreset.BottomRight => Size,
            OriginPreset.BottomCenter => new(Size.X / 2, Size.Y),
            OriginPreset.None => Origin,
            _ => Origin,
        };
    }

    // Left click

    private void HandleLeftClicks()
    {
        if (LeftClicked is null)
        {
            return;
        }

        if (LeftClickMode == ButtonClickMode.Limitless)
        {
            HandleLeftClickLimitless();
        }
        else
        {
            HandleLeftClickLimited();
        }
    }

    private void HandleLeftClickLimitless()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (!IsMouseOver())
            {
                alreadyClicked = true;
            }
        }

        if (IsMouseOver())
        {
            Style.Current = Style.Hover;

            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                if (!alreadyClicked && OnTopLeft)
                {
                    OnTopLeft = false;
                    PressedLeft = true;
                    alreadyClicked = true;
                }

                if (PressedLeft)
                {
                    Style.Current = Style.Pressed;
                }
            }
        }
        else
        {
            Style.Current = Style.Default;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            if (IsMouseOver() && PressedLeft)
            {
                LeftClicked?.Invoke(this, EventArgs.Empty);
            }

            PressedLeft = false;
            alreadyClicked = false;
            Style.Current = Style.Default;
        }
    }

    private void HandleLeftClickLimited()
    {
        if (IsMouseOver())
        {
            Style.Current = Style.Hover;

            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && OnTopLeft)
            {
                PressedLeft = true;
                OnTopLeft = false;
            }

            if (PressedLeft)
            {
                Style.Current = Style.Pressed;
            }
        }
        else
        {
            PressedLeft = false;
            Style.Current = Style.Default;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            if (IsMouseOver() && PressedLeft)
            {
                LeftClicked?.Invoke(this, EventArgs.Empty);
            }

            PressedLeft = false;
            Style.Current = Style.Default;
        }
    }

    // Right click

    private void HandleRightClicks()
    {
        if (RightClicked is null)
        {
            return;
        }

        if (RightClickMode == ButtonClickMode.Limitless)
        {
            HandleRightClickLimitless();
        }
        else
        {
            HandleRightClickLimited();
        }
    }

    private void HandleRightClickLimitless()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            if (!IsMouseOver())
            {
                alreadyClicked = true;
            }
        }

        if (IsMouseOver())
        {
            Style.Current = Style.Hover;

            if (Raylib.IsMouseButtonDown(MouseButton.Right))
            {
                if (!alreadyClicked && OnTopRight)
                {
                    OnTopRight = false;
                    PressedRight = true;
                    alreadyClicked = true;
                }

                if (PressedRight)
                {
                    Style.Current = Style.Pressed;
                }
            }
        }
        else
        {
            Style.Current = Style.Default;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Right))
        {
            if (IsMouseOver() && PressedRight)
            {
                RightClicked?.Invoke(this, EventArgs.Empty);
            }

            PressedRight = false;
            alreadyClicked = false;
            Style.Current = Style.Default;
        }
    }

    private void HandleRightClickLimited()
    {
        if (IsMouseOver())
        {
            Style.Current = Style.Hover;

            if (Raylib.IsMouseButtonPressed(MouseButton.Right) && OnTopRight)
            {
                PressedRight = true;
                OnTopRight = false;
            }

            if (PressedRight)
            {
                Style.Current = Style.Pressed;
            }
        }
        else
        {
            PressedRight = false;
            Style.Current = Style.Default;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Right))
        {
            if (IsMouseOver() && PressedRight)
            {
                RightClicked?.Invoke(this, EventArgs.Empty);
            }

            PressedRight = false;
            Style.Current = Style.Default;
        }
    }

    // Draw

    private void Draw()
    {
        if (Visible)
        {
            DrawShape();
            DrawText();
        }
    }

    private void DrawShape()
    {
        DrawOutline();
        DrawInside();
    }

    private void DrawInside()
    {
        Rectangle rectangle = new()
        {
            Position = GlobalPosition - Origin,
            Size = Size
        };

        Raylib.DrawRectangleRounded(
            rectangle, 
            Style.Current.Roundness, 
            (int)Size.Y, 
            Style.Current.FillColor);
    }

    private void DrawOutline()
    {
        if (Style.Current.OutlineThickness < 0)
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
                Style.Current.Roundness,
                (int)Size.Y,
                Style.Current.OutlineColor);
        }
    }

    private void DrawText()
    {
        Raylib.DrawTextEx(
            Style.Current.Font, 
            Text, 
            GetTextPosition(), 
            Style.Current.FontSize, 
            1, 
            Style.Current.TextColor);
    }

    //private Vector2 GetTextPosition()
    //{
    //    Vector2 fontDimensions = Raylib.MeasureTextEx(
    //                                Style.Current.Font,
    //                                Text,
    //                                Style.Current.FontSize,
    //                                1);
    //
    //    Vector2 halfFontDimensions = fontDimensions / 2;
    //    Vector2 center = Size / 2;
    //
    //    if (Text == "PlaylistButton")
    //    {
    //        Console.WriteLine("origin: " + Origin);
    //        Console.WriteLine("trigin: " + TextOrigin);
    //        Console.WriteLine("center: " + center);
    //    }
    //
    //    Vector2 difference = TextOrigin - center;
    //
    //    //return GlobalPosition + center - halfFontDimensions - Origin; // best
    //    return GlobalPosition + center - /*halfFontDimensions*/ - Origin + difference; // second best
    //}

    private Vector2 GetTextPosition()
    {
        // Measure the dimensions of the text
        Vector2 fontDimensions = Raylib.MeasureTextEx(
            Style.Current.Font,
            Text,
            Style.Current.FontSize,
            1
        );

        // Calculate the center of the button
        Vector2 center = Size / 2;

        // Determine the alignment adjustment based on the TextOrigin
        Vector2 alignmentAdjustment = new(
            TextOrigin.X < center.X ? 0 : TextOrigin.X > center.X ? -fontDimensions.X : -fontDimensions.X / 2,
            TextOrigin.Y < center.Y ? 0 : TextOrigin.Y > center.Y ? -fontDimensions.Y : -fontDimensions.Y / 2
        );

        // Calculate the text position based on the alignment and origin
        return GlobalPosition + TextOrigin + alignmentAdjustment - Origin + TextPadding;
    }
}