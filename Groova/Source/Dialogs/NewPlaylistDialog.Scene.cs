namespace Groova;

public partial class NewPlaylistDialog : Node2D
{
    public override void Build()
    {
        AddChild(new ColoredRectangle
        {
            Size = new(300, 150),
            InheritsOrigin = true
        });

        AddChild(new Button
        {
            Text = "X",
            Size = new(25, 25),
            InheritsOrigin = true,
            Layer = ClickableLayer.DialogButtons,
            Style = new()
            {
                TextColor = Color.Red
            },
            OnUpdate = (button) =>
            {
                float x = 300 - 35;
                float y = 10;

                button.Position = new(x, y);
            }
        });

        AddChild(new Label
        {
            Position = new(10, 20),
            InheritsOrigin = true,
            Text = "Enter playlist name:"
        });

        AddChild(new TextBox
        {
            Position = new(0, 10),
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
        });
    }
}