namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private Label timePlayedLabel;
    private Label totalTimeLabel;
    private SongPlayer musicPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetChild<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPauseOrResumeButtonLeftClicked;

        musicPlayer = GetNode<SongPlayer>("SongPlayer");

        timePlayedLabel = GetChild<Label>("TimePlayedLabel");
        totalTimeLabel = GetChild<Label>("TotalTimeLabel");
    }

    public override void Update()
    {
        timePlayedLabel.Text = FormatTime(musicPlayer.TimePlayed);
        totalTimeLabel.Text = FormatTime(musicPlayer.AudioLength);
        base.Update();
    }

    private void OnPauseOrResumeButtonLeftClicked(object? sender, EventArgs e)
    {
        PauseOrResume();
    }

    private void PauseOrResume()
    {
        if (musicPlayer.Playing)
        {
            musicPlayer.Pause();
        }
        else
        {
            musicPlayer.Resume();
        }

        pauseOrResumeButton.Text = pauseOrResumeButton.Text == ">" ?
                                   "||" :
                                   ">";
    }

    private static string FormatTime(float seconds)
    {
        if (seconds < 0)
        {
            seconds = 0;
        }

        int minutes = (int)(seconds / 60);
        int remainingSeconds = (int)(seconds % 60);

        return $"{minutes:D2}:{remainingSeconds:D2}";
    }
}