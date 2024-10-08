﻿namespace Groova;

public abstract partial class BaseItem : Node2D
{
    public override void Build()
    {
        AddChild(new Button
        {
            Size = new(100, 40),
            OriginPreset = OriginPreset.TopLeft,
            TextOriginPreset = OriginPreset.CenterLeft,
            LimitText = true,
            TextPadding = new(50, 0),
            Style = new()
            {
                Roundness = 0
            },
            OnUpdate = (button) =>
            {
                float width = Window.Width - 35;
                float height = button.Size.Y;
                button.Size = new(width, height);

                button.AvailableWidth = button.Size.X - 100;
            }
        });

        AddChild(new Button
        {
            Position = new(24, 20),
            Size = new(32, 32),
            Style = new()
            {
                OutlineThickness = 0,
                Roundness = 0,
                FillColor = ThemeLoader.Instance.Colors["Accent"]
            }
        }, "ImageButton");

        AddChild(new TexturedRectangle
        {
            Position = new(24, 20),
        }, "Image");
    }
}