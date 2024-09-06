namespace Groova;

public partial class CurrentSongDisplayer : Node2D
{
    public Button Button;

    private Label label;
    private TexturedRectangle image;
    private MainScene mainScene;

    public override void Start()
    {
        image = GetNode2<TexturedRectangle>("Image");
        label = GetNode2<Label>("SongName");

        Button = GetNode2<Button>("ReplayModeButton");
        Button.LeftClicked += OnButtonLeftClicked;

        mainScene = GetNode2<MainScene>("/root");

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

        string replayMode = "Stop";

        switch (Button.Text)
        {
            case "Stop":
                replayMode = "Repeat";
                break;

            case "Repeat":
                replayMode = "Loop";
                break;

            case "Loop":
                replayMode = "Shuffle";
                break;

            case "Shuffle":
                replayMode = "Stop";
                break;
        }

        Button.Text = replayMode;
        songPlayer.ReplayMode = replayMode;

        mainScene.Settings.ReplayMode = replayMode;
        mainScene.SaveSettings();
    }
}