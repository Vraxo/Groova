namespace Groova;

public partial class TopSection : Node2D
{
    public override void Build()
    {
        AddChild(new TextBox
        {
            Position = new(0, 25),
            PlaceholderText = "Search...",
            Style = new()
            {
                Roundness = 1
            },
            OnUpdate = (textBox) =>
            {
                float x = Window.Width / 2;
                float y = textBox.Position.Y;
                textBox.Position = new(x, y);

                float width = Window.Width * 0.75f;
                float height = textBox.Size.Y;
                textBox.Size = new(width, height);
            }
        }, "SearchBar");

        AddChild(new Button
        {
            Position = new(20, 25),
            Size = new(24, 24),
            Text = "+",
        }, "AddButton");

        AddChild(new Button
        {
            Position = new(20, 25),
            Size = new(24, 24),
            Text = "<-",
            OnUpdate = (button) =>
            {
                float x = Window.Width - 20;
                float y = button.Position.Y;

                button.Position = new(x, y);
            }
        }, "ReturnButton");
    }
}