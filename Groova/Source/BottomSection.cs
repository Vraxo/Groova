namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private Label timePlayedLabel;
    private Label totalTimeLabel;
    private SongPlayer songPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetNode<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPauseOrResumeButtonLeftClicked;

        songPlayer = GetNode<SongPlayer>("/root/SongPlayer");

        timePlayedLabel = GetNode<Label>("TimePlayedLabel");
        totalTimeLabel = GetNode<Label>("TotalTimeLabel");
    }

    public override void Update()
    {
        timePlayedLabel.Text = FormatTime(songPlayer.TimePlayed);
        totalTimeLabel.Text = FormatTime(songPlayer.AudioLength);
        base.Update();
    }

    public void LoadSettings(Settings settings, bool setAudioSlider)
    {
        pauseOrResumeButton.Text = ">";

        if (setAudioSlider)
        {
            var audioSlider = GetNode<HorizontalSlider>("AudioSlider");
            audioSlider.Percentage = settings.Timestamp;
            audioSlider.InitialPercentage = settings.Timestamp;
        }

        var pitchSlider = GetNode<HorizontalSlider>("PitchSlider");
        pitchSlider.Percentage = settings.Pitch;
        pitchSlider.InitialPercentage = settings.Pitch;

        var volumeSlider = GetNode<HorizontalSlider>("VolumeSlider");
        volumeSlider.Percentage = settings.Volume;
        volumeSlider.InitialPercentage = settings.Volume;

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
            if (songPlayer.TimePlayed > 0)
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