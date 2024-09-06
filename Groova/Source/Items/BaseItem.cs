﻿namespace Groova;

public abstract partial class BaseItem : Node2D
{
    protected string Text;
    protected TexturedRectangle image;
    protected PlaylistContainer playlistsContainer;

    public override void Start()
    {
        var button = GetChild<Button>();
        //button.OriginalText = Text;
        button.Text = Text;
        button.LeftClicked += OnButtonLeftClicked;
        button.RightClicked += OnButtonRightClicked;

        var imageButton = GetChild<Button>("ImageButton");
        imageButton.LeftClicked += OnImageButtonLeftClicked;
        imageButton.RightClicked += OnImageButtonRightClicked;

        image = GetChild<TexturedRectangle>();
        playlistsContainer = GetNode<PlaylistContainer>("PlaylistContainer");
    }

    protected abstract void OnButtonLeftClicked(object? sender, EventArgs e);
    protected abstract void OnButtonRightClicked(object? sender, EventArgs e);
    
    protected abstract void SetImage(string imagePath);

    private void OnImageButtonLeftClicked(object? sender, EventArgs e)
    {
        OpenFileDialog dialog = new();
        dialog.ShowDialog();

        if (dialog.FileName != string.Empty)
        {
            SetImage(dialog.FileName);
            image.Load(dialog.FileName);
        }
    }

    private void OnImageButtonRightClicked(object? sender, EventArgs e)
    {
        SetImage("");
        image.Load("");
    }
}