﻿namespace Groova;

public partial class VerticalSlider : BaseSlider
{
    public override void Build()
    {
        VerticalGrabber grabber = new();
        AddChild(grabber, "Grabber");

        AddChild(new Button
        {
            Position = new(0, grabber.Size.Y * -2),
            Size = new(10, 10),
            Layer = ClickableLayer.PanelButtons,
            OnUpdate = (button) =>
            {
                float x = button.Position.X;
                float y = -Origin.Y - grabber.Size.Y * 2;

                button.Position = new(x, y);
            }
        }, "DecrementButton");

        AddChild(new Button
        {
            Size = new(10, 10),
            Layer = ClickableLayer.PanelButtons,
            OnUpdate = (button) =>
            {
                float x = button.Position.X;
                float y = Size.Y - Origin.Y + grabber.Size.Y * 2 - 1;

                button.Position = new(x, y);
                
            },
        }, "IncrementButton");
    }
}