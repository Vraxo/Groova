namespace Groova;

public class Theme
{
    public Dictionary<string, Color> Colors { get; } = new()
    {
        { "Background", new(16, 16, 16, 255) },
        { "DefaultFill", new(42, 42, 42, 255) },
        { "DefaultOutline", new(61, 61, 61, 255) },
        { "HoverFill", new(50, 50, 50, 255) },
        { "HoverOutline", new(71, 71, 71, 255) },
        { "Accent", new(71, 114, 179, 255) },
        { "PressedOutline", new(61, 61, 61, 255) },
        { "TextBoxPressedFill", new(68, 68, 68, 255) }
    };

    private static Theme? instance;

    public static Theme Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }
}