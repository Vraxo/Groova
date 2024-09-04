using Raylib_cs;

namespace Groova;

public partial class CurrentSongDisplayer : Node2D
{
    public Action<CurrentSongDisplayer> OnUpdate = (displayer) => { };

    private string originalText = "";
    private Label label;
    private TexturedRectangle image;

    public override void Start()
    {
        image = GetChild<TexturedRectangle>();
        label = GetChild<Label>();

        base.Start();
    }

    public override void Update()
    {
        OnUpdate(this);
        UpdateLabel();
        base.Update();
    }

    public void SetSong(Song song)
    {
        originalText = $"Now playing: {song.GetName()}";
        image.Load(song.ImagePath);
    }

    private void UpdateLabel()
    {
        float availableWidth = Raylib.GetScreenWidth() - 125;
        float characterWidth = GetCharacterWidth();
        int numFittingCharacters = (int)(availableWidth / characterWidth);

        if (numFittingCharacters <= 0)
        {
            label.Text = "";
        }
        else if (numFittingCharacters < originalText.Length)
        {
            string trimmedText = originalText[..numFittingCharacters];
            label.Text = ReplaceLastThreeWithDots(trimmedText);
        }
        else
        {
            label.Text = originalText;
        }
    }

    private float GetCharacterWidth()
    {
        float width = Raylib.MeasureTextEx(
            label.Font,
            " ",
            label.FontSize,
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