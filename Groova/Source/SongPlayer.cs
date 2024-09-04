namespace Groova;

public class SongPlayer : AudioPlayer
{
    public Song Song;
    public Playlist Playlist;
    public string State = "Stop";

    private HorizontalSlider slider;
    private CurrentSongDisplayer currentSongDisplayer;

    public override void Ready()
    {
        AudioFinished += OnAudioFinished;

        slider = GetNode<HorizontalSlider>("BottomSection/AudioSlider");
        slider.Released += OnAudioSliderReleased;

        var volumeSlider = GetNode<HorizontalSlider>("BottomSection/VolumeSlider");
        volumeSlider.PercentageChanged += OnVolumeSliderPercentageChanged;

        var pitchSlider = GetNode<HorizontalSlider>("BottomSection/PitchSlider");
        pitchSlider.PercentageChanged += OnPitchSliderPercentageChanged;

        currentSongDisplayer = GetNode<CurrentSongDisplayer>("BottomSection/CurrentSongDisplayer");

        base.Ready();
    }

    private void OnAudioFinished(object? sender, EventArgs e)
    {
        slider.Percentage = 0;

        switch (State)
        {
            case "Stop":
                break;

            case "Repeat":
                Play();
                break;

            case "Loop":
                Loop();
                break;

            case "Shuffle":
                Shuffle();
                break;
        }
    }

    private void OnPitchSliderPercentageChanged(object? sender, float e)
    {
        float exponent = 1f;
        float factor = (float)Math.Pow(2, (e - 0.5f) * exponent);

        Pitch = factor;
    }

    private void OnAudioSliderReleased(object? sender, float e)
    {
        float timestamp = e * AudioLength;
        Play(timestamp);
    }

    private void OnVolumeSliderPercentageChanged(object? sender, float e)
    {
        Volume = e;
    }

    public override void Update()
    {
        if (Playing)
        {
            slider.Percentage = TimePlayed / AudioLength;
        }

        base.Update();
    }

    private void Loop()
    {
        int index = Playlist.Songs.IndexOf(Song) + 1;

        if (index >= 0 && index < Playlist.Songs.Count)
        {
            Song = Playlist.Songs[index];
        }
        else
        {
            Song = Playlist.Songs.First();
        }

        Load(Song.FilePath);
        Play();

        currentSongDisplayer.SetSong(Song);

        Console.WriteLine("looped");
    }

    private void Shuffle()
    {
        Random random = new();
        Song randomSong;

        do
        {
            int randomIndex = random.Next(Playlist.Songs.Count);
            randomSong = Playlist.Songs[randomIndex];
        } 
        while (randomSong == Song && Playlist.Songs.Count > 1);

        Load(randomSong.FilePath);
        Play();

        currentSongDisplayer.SetSong(randomSong);
    }
}