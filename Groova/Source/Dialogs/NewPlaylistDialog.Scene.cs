﻿namespace Groova;

public partial class NewPlaylistDialog : Node2D
{
    public override void Build()
    {
        AddChild(new Panel
        {
            Size = new(300, 150),
            InheritsOrigin = true
        });

        AddChild(new Button
        {
            Text = "X",
            Size = new(25, 25),
            InheritsOrigin = true,
            Layer = ClickableLayer.DialogButtons,
            Style = new()
            {
                TextColor = Color.Red
            },
            OnUpdate = (button) =>
            {
                float x = 400 - 35;
                float y = 10;

                button.Position = new(x, y);
            }
        });

        AddChild(new Label
        {
            Position = new(10, 20),
            InheritsOrigin = true,
            Text = "Enter playlist name:"
        });

        AddChild(new TextBox
        {
            Position = new(0, 10),
            Size = new(250, 25),
            Style = new()
            {
                Roundness = 1
            }
        });
    }
}