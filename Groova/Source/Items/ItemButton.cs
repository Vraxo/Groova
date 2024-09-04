using Raylib_cs;

namespace Groova;

public class ItemButton : Button
{
    public string OriginalText;

    public override void Update()
    {
        float availableWidth = Size.X - 100;
        float characterWidth = GetCharacterWidth();
        int numFittingCharacters = (int)(availableWidth / characterWidth);

        if (numFittingCharacters <= 0)
        {
            Text = "";
        }
        else if (numFittingCharacters < OriginalText.Length)
        {
            string trimmedText = OriginalText[..numFittingCharacters];
            Text = ReplaceLastThreeWithDots(trimmedText);
        }
        else
        {
            Text = OriginalText;
        }

        base.Update();
    }

    private float GetCharacterWidth()
    {
        float width = Raylib.MeasureTextEx(
            Style.Current.Font,
            " ",
            Style.Current.FontSize,
            1).X;

        return width;
    }

    private static string ReplaceLastThreeWithDots(string input)
    {
        if (input.Length > 3)
        {
            string trimmedText = input[..^3];
            return trimmedText + "...";
        }
        else
        {
            return input; // If the string is too short, don't replace with dots.
        }
    }
}