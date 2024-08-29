namespace Groova;

public partial class ConfirmDialog : Dialog
{
    public Playlist Playlist;
    public string SongPath;

    public override void Start()
    {
        GetChild<Button>("ConfirmButton").LeftClicked += OnConfirmButtonLeftClicked;
        base.Start();
    }

    protected virtual void OnConfirmButtonLeftClicked(object? sender, EventArgs e)
    {
    }
}