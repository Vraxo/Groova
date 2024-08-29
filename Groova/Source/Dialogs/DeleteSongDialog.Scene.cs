namespace Groova;

public partial class DeleteSongDialog : Dialog
{
    public override void Build()
    {
        base.Build();

        AddChild(new Button
        {
            Position = new(0, 50),
            Layer = ClickableLayer.DialogButtons,
            Text = "Delete",
            Style = new()
            {
                TextColor = new(255, 0, 0, 255)
            }
        }, "DeleteButton");
    }
}