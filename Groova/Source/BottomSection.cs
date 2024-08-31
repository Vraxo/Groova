namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private SongPlayer musicPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetChild<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPauseOrResumeButtonLeftClicked;

        GetChild<HorizontalSlider>("PitchSlider").SizeChanged += OnPitchSliderSizeChanged;
        GetChild<HorizontalSlider>("VolumeSlider").SizeChanged += OnVolumeSliderSizeChanged;

        musicPlayer = GetNode<SongPlayer>("SongPlayer");
    }

    private void OnPitchSliderSizeChanged(object? sender, EventArgs e)
    {
        var pitchSlider = GetChild<HorizontalSlider>("PitchSlider");
        pitchSlider.Percentage = pitchSlider.Percentage;
    }

    private void OnVolumeSliderSizeChanged(object? sender, EventArgs e)
    {
        var volumeSlider = GetChild<HorizontalSlider>("VolumeSlider");
        volumeSlider.Percentage = volumeSlider.Percentage;
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
}