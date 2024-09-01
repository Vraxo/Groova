namespace Groova;

public class TextBoxText : TextBoxBaseText
{
    protected override string GetText()
    {
        return parent.Text;
    }

    protected override bool ShouldSkipDrawing()
    {
        return false;
    }
}