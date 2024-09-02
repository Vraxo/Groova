namespace Groova;

public class ButtonStateStyle
{
    public float TextSpacing { get; set; } = 1;

    public float Roundness { get; set; } = 0F;
    public float OutlineThickness { get; set; } = 0;
    public float FontSize { get; set; } = 16;
    public Font Font { get; set; } = FontLoader.Instance.Fonts["RobotoMono 32"];
    public Color TextColor { get; set; } = Color.White;
    public Color FillColor { get; set; } = Theme.DefaultFill;
    public Color OutlineColor { get; set; } = Theme.DefaultOutline;
}