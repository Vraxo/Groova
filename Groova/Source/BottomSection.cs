namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private SongPlayer musicPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetChild<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPauseOrResumeButtonLeftClicked;

        musicPlayer = GetNode<SongPlayer>("SongPlayer");
    }

    public override void Update()
    {
        GetChild<Label>().Text = FormatTime(musicPlayer.TimePlayed);

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
        // Ensure seconds is not negative
        if (seconds < 0) seconds = 0;

        // Calculate minutes and remaining seconds
        int minutes = (int)(seconds / 60);
        int remainingSeconds = (int)(seconds % 60);

        // Format and return as MM:SS
        return $"{minutes:D2}:{remainingSeconds:D2}";
    }

}