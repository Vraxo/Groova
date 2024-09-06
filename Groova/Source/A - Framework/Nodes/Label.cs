using Raylib_cs;

namespace Groova;

public class Label : Node2D
{
    public Color Color { get; set; } = Color.White;
    public uint FontSize { get; set; } = 16;
    public Font Font { get; set; } = FontLoader.Instance.Fonts["RobotoMono 32"];
    public bool LimitText { get; set; } = false;
    public float AvailableWidth { get; set; } = 0;
    public Action<Label> OnUpdate = (label) => { };

    private string _text = "";
    public string Text
    {
        get => _text;

        set
        {
            _text = value;
            displayedText = value;
        }
    }

    private string displayedText = "";

    public Label()
    {
        OriginPreset = OriginPreset.CenterLeft;
    }

    public override void Update()
    {
        OnUpdate(this);
        UpdateSize();
        UpdateLabel();
        Draw();
        base.Update();
    }

    private void UpdateSize()
    {
        Size = Raylib.MeasureTextEx(
            Font, 
            Text, 
            FontSize, 
            1);
    }

    private void Draw()
    {
        if (!(Visible && readyForVisibility))
        {
            return;
        }

        Raylib.DrawTextEx(
            Font, 
            displayedText, 
            GlobalPosition - Origin, 
            FontSize, 
            1,
            Color);
    }

    private void UpdateLabel()
    {
        if (!LimitText)
        {
            return;
        }

        float characterWidth = GetCharacterWidth();
        int numFittingCharacters = (int)(AvailableWidth / characterWidth);

        if (numFittingCharacters <= 0)
        {
            displayedText = "";
        }
        else if (numFittingCharacters < Text.Length)
        {
            string trimmedText = Text[..numFittingCharacters];
            displayedText = ReplaceLastThreeWithDots(trimmedText);
        }
        else
        {
            displayedText = Text;
        }
    }

    private float GetCharacterWidth()
    {
        float width = Raylib.MeasureTextEx(
            Font,
            " ",
            FontSize,
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
            return input;
        }
    }
}