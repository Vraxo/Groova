using Raylib_cs;

namespace Groova;

public partial class Dialog : Node2D
{
    public override void Start()
    {
        Origin = GetChild<ColoredRectangle>().Size / 2;
        GetNode<ClickManager>().MinLayer = ClickableLayer.DialogButtons;
        GetChild<Button>("CloseButton").LeftClicked += OnCloseButtonLeftClicked;
    }

    public override void Update()
    {
        UpdatePosition();
    }

    protected void Close()
    {
        GetNode<ClickManager>().MinLayer = 0;
        Destroy();
    }

    private void OnCloseButtonLeftClicked(object? sender, EventArgs e)
    {
        Close();
    }

    private void UpdatePosition()
    {
        float x = Window.Width/ 2;
        float y = Window.Height / 2;

        Position = new(x, y);
    }
}