namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private Label timePlayedLabel;
    private Label totalTimeLabel;
    private SongPlayer songPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetChild<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPauseOrResumeButtonLeftClicked;

        songPlayer = GetNode<SongPlayer>("SongPlayer");

        timePlayedLabel = GetChild<Label>("TimePlayedLabel");
        totalTimeLabel = GetChild<Label>("TotalTimeLabel");
    }

    public override void Update()
    {
        timePlayedLabel.Text = FormatTime(songPlayer.TimePlayed);
        totalTimeLabel.Text = FormatTime(songPlayer.AudioLength);
        base.Update();
    }

    public void LoadSettings(Settings settings)
    {
        pauseOrResumeButton.Text = ">";

        GetChild<HorizontalSlider>("AudioSlider").Percentage = settings.Timestamp;
        GetChild<HorizontalSlider>("AudioSlider").InitialPercentage = settings.Timestamp;

        GetChild<HorizontalSlider>("PitchSlider").Percentage = settings.Pitch;
        GetChild<HorizontalSlider>("PitchSlider").InitialPercentage = settings.Pitch;

        GetChild<HorizontalSlider>("VolumeSlider").Percentage = settings.Volume;
        GetChild<HorizontalSlider>("VolumeSlider").InitialPercentage = settings.Volume;

        songPlayer.Volume = settings.Volume;

        float exponent = 1f;
        float factor = (float)Math.Pow(2, (settings.Pitch - 0.5f) * exponent);
        songPlayer.Pitch = factor;
    }

    private void OnPauseOrResumeButtonLeftClicked(object? sender, EventArgs e)
    {
        PauseOrResume();
    }

    private void PauseOrResume()
    {
        if (pauseOrResumeButton.Text == ">")
        {
            if (songPlayer.TimePlayed > -1)
            {
                songPlayer.Resume();
            }
            else
            {
                songPlayer.Play();
            }

            pauseOrResumeButton.Text = "||";
        }
        else
        {
            songPlayer.Pause();
            pauseOrResumeButton.Text = ">";
        }
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