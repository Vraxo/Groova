﻿namespace Groova;

public class ButtonStyle
{
    public ButtonStyle()
    {
        Current = Default;
        Roundness = 1F;
        //OutlineThickness = 0;
    }

    public ButtonStateStyle Current { get; set; } = new();

    public ButtonStateStyle Default { get; set; } = new();

    public ButtonStateStyle Hover { get; set; } = new()
    {
        FillColor = Theme.Instance.Colors["HoverFill"],
        OutlineColor = Theme.Instance.Colors["HoverOutline"]
    };

    public ButtonStateStyle Pressed { get; set; } = new()
    {
        FillColor = Theme.Instance.Colors["Accent"],
        OutlineColor = Theme.Instance.Colors["PressedOutline"],
    };

    public float Roundness
    {
        set
        {
            Current.Roundness = value;
            Hover.Roundness = value;
            Pressed.Roundness = value;
            Default.Roundness = value;
        }
    }

    public float OutlineThickness
    {
        set
        {
            Hover.OutlineThickness = value;
            Pressed.OutlineThickness = value;
            Default.OutlineThickness = value;
        }
    }

    public float FontSize
    {
        set
        {
            Hover.FontSize = value;
            Pressed.FontSize = value;
            Default.FontSize = value;
        }
    }

    public Font Font
    {
        set
        {
            Hover.Font = value;
            Pressed.Font = value;
            Default.Font = value;
        }
    }

    public Color TextColor
    {
        set
        {
            Hover.TextColor = value;
            Pressed.TextColor = value;
            Default.TextColor = value;
        }
    }

    public Color FillColor
    {
        set
        {
            Hover.FillColor = value;
            Pressed.FillColor = value;
            Default.FillColor = value;
        }
    }

    public Color OutlineColor
    {
        set
        {
            Hover.OutlineColor = value;
            Pressed.OutlineColor = value;
            Default.OutlineColor = value;
        }
    }
}