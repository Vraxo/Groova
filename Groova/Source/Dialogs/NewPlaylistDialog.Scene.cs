namespace Groova;

public partial class NewPlaylistDialog : Dialog
{
    public override void Build()
    {
        base.Build();

        AddChild(new TextBox
        {
            Position = new(0, 25),
            Size = new(250, 25),
            Layer = ClickableLayer.DialogButtons,
            Style = new()
            {
                Roundness = 1
            },
            MaxCharacters = 30
        });

        AddChild(new Label
        {
            Position = new(10, 125),
            InheritsOrigin = true,
            Visible = false,
            Color = new(255, 0, 0, 255),
            Text = "Playlist already exists."
        }, "ErrorLabel");
    }
}