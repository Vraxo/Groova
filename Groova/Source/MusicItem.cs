using Raylib_cs;

namespace Groova;

public partial class MusicItem : Node2D
{
    public string MusicPath = "";

    private Button button;

    public override void Start()
    {
        button = GetChild<Button>();
        button.Text = Path.GetFileNameWithoutExtension(MusicPath);
        button.LeftClicked += OnButtonLeftclicked;
    }

    private void OnButtonLeftclicked(object? sender, EventArgs e)
    {
        var musicPlayer = GetNode<MusicPlayer>("MusicPlayer");

        musicPlayer.Audio = Raylib.LoadMusicStream(MusicPath);
        musicPlayer.Play();
    }
}