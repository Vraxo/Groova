﻿namespace Groova;

public class TextBoxPlaceholderText : TextBoxBaseText
{
    protected override string GetText()
    {
        return parent.PlaceholderText;
    }

    protected override bool ShouldSkipDrawing()
    {
        return parent.Text.Length > 0;
    }
}