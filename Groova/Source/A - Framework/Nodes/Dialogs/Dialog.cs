﻿namespace Groova;

public partial class Dialog : Node2D
{
    public override void Start()
    {
        Origin = GetNode2<ColoredRectangle>("Background").Size / 2;
        GetNode2<ClickManager>("/root/ClickManager").MinLayer = ClickableLayer.DialogButtons;
        GetNode2<Button>("CloseButton").LeftClicked += OnCloseButtonLeftClicked;
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