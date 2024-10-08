﻿namespace Groova;

public class SongPlayer : AudioPlayer
{
    public Song Song;
    public string ReplayMode = "Stop";

    private HorizontalSlider slider;
    private CurrentSongDisplayer currentSongDisplayer;
    private MainScene mainScene;
    private Button pauseOrResumeButton;

    private Playlist _playlist;
    public Playlist Playlist
    {
        get => _playlist;

        set
        {
            _playlist = value;

            mainScene.Settings.Playlist = value.Name;
            mainScene.SaveSettings();
        }
    }

    public override void Start()
    {
        AudioFinished += OnAudioFinished;

        mainScene = GetNode<MainScene>("/root");

        base.Start();
    }

    public override void Ready()
    {
        pauseOrResumeButton = GetNode1000<Button>("BottomSection/PauseOrResumeButton");

        slider = GetNode<HorizontalSlider>("/root/BottomSection/AudioSlider");
        slider.Released += OnAudioSliderReleased;

        var volumeSlider = GetNode<HorizontalSlider>("/root/BottomSection/VolumeSlider");
        volumeSlider.PercentageChanged += OnVolumeSliderPercentageChanged;

        var pitchSlider = GetNode<HorizontalSlider>("/root/BottomSection/PitchSlider");
        pitchSlider.PercentageChanged += OnPitchSliderPercentageChanged;

        currentSongDisplayer = GetNode<CurrentSongDisplayer>("/root/BottomSection/CurrentSongDisplayer");

        base.Ready();
    }

    public override void Update()
    {
        if (Playing)
        {
            if (!slider.Grabber.Pressed)
            {
                slider.Percentage = TimePlayed / AudioLength;
            }

            mainScene.Settings.Timestamp = slider.Percentage;
            mainScene.SaveSettings();
        }

        base.Update();
    }

    private void OnAudioFinished(object? sender, EventArgs e)
    {
        slider.Percentage = 0;

        switch (ReplayMode)
        {
            case "Stop":
                pauseOrResumeButton.Text = ">";
                break;

            case "Repeat":
                Play();
                break;

            case "Loop":
                DoLoop();
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

        mainScene.Settings.Pitch = e;
        mainScene.SaveSettings();
    }

    private void OnAudioSliderReleased(object? sender, float e)
    {
        float timestamp = e * AudioLength;

        if (Playing)
        {
            Play(timestamp);
        }
        else
        {
            Seek(timestamp);
            slider.Percentage = e;
        }

        mainScene.Settings.Timestamp = e;
        mainScene.SaveSettings();
    }

    private void OnVolumeSliderPercentageChanged(object? sender, float e)
    {
        Volume = e;

        mainScene.Settings.Volume = e;
        mainScene.SaveSettings();
    }

    private void DoLoop()
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

        LoadAndPlaySong(Song);
        currentSongDisplayer.SetSong(Song);
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

        LoadAndPlaySong(randomSong);
        currentSongDisplayer.SetSong(randomSong);
    }

    public void LoadAndPlaySong(Song song)
    {
        Load(song.FilePath);
        Play();

        pauseOrResumeButton.Text = "||";

        Song = song;

        mainScene.Settings.Song = Song.FilePath;
        mainScene.SaveSettings();
    }
}