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

    private void OnButtonLeftClicked(object? sender, EventArgs e)
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

    public override void Update()
    {
        float x = Position.X;
        float y = Window.Height - 110;
        Position = new(x, y);
    }

    public void SetSong(Song song)
    {
        label.Text = song.GetName();
        image.Load(song.ImagePath);
    }
}