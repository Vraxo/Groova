namespace Groova;

public class Theme
{
    public Color DefaultFill = new(42, 42, 42, 255);
    public Color DefaultOutline = new(61, 61, 61, 255);
    public Color HoverFill = new(50, 50, 50, 255);
    public Color HoverOutline = new(71, 71, 71, 255);
    public Color Accent = new(71, 114, 179, 255);
    public Color PressedOutline = new(61, 61, 61, 255);

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