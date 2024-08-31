namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private SongPlayer musicPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetChild<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPauseOrResumeButtonLeftClicked;

        GetChild<HorizontalSlider>("PitchSlider").Released += OnPitchSliderReleased;

        musicPlayer = GetNode<SongPlayer>("SongPlayer");
        GetChild<HorizontalSlider>("PitchSlider").MoveGrabberTo(1);
    }

    private void OnPitchSliderReleased(object? sender, float e)
    {
        musicPlayer.Pitch = e * 2;
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