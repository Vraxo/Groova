namespace Groova;

public partial class CurrentSongDisplayer : Node2D
{
    private Label label;
    private TexturedRectangle image;
    private Button button;

    public override void Start()
    {
        image = GetChild<TexturedRectangle>();
        label = GetChild<Label>();

        button = GetChild<Button>();
        button.LeftClicked += OnButtonLeftClicked;

        base.Start();
    }

    public override void Update()
    {
        UpdatePosition();
        base.Update();
    }

    public void SetSong(Song song)
    {
        label.Text = song.GetName();
        image.Load(song.ImagePath);
    }

    private void OnButtonLeftClicked(object? sender, EventArgs e)
    {
        SwitchReplayMode();
    }

    private void UpdatePosition()
    {
        float x = Position.X;
        float y = Window.Height - 110;
        Position = new(x, y);
    }

    private void SwitchReplayMode()
    {
        var songPlayer = GetNode<SongPlayer>("SongPlayer");

        string state = "Stop";

        switch (button.Text)
        {
            case "Stop":
                state = "Repeat";
                break;

            case "Repeat":
                state = "Loop";
                break;

            case "Loop":
                state = "Shuffle";
                break;

            case "Shuffle":
                state = "Stop";
                break;
        }

        button.Text = state;
        songPlayer.State = state;
    }
}