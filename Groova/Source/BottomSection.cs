namespace Groova;

public partial class BottomSection : Node2D
{
    private Button pauseOrResumeButton;
    private MusicPlayer musicPlayer;

    public override void Start()
    {
        pauseOrResumeButton = GetChild<Button>("PauseOrResumeButton");
        pauseOrResumeButton.LeftClicked += OnPlayButtonLeftClicked;

        musicPlayer = GetNode<MusicPlayer>("MusicPlayer");
    }

    private void OnPlayButtonLeftClicked(object? sender, EventArgs e)
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