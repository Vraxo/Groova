namespace Groova;

public class SongPlayer : AudioPlayer
{
    private HorizontalSlider slider;

    public override void Ready()
    {
        slider = GetNode<HorizontalSlider>("BottomSection/AudioSlider");
        slider.Released += OnSliderReleased;

        var volumeSlider = GetNode<HorizontalSlider>("BottomSection/VolumeSlider");
        volumeSlider.PercentageChanged += OnVolumeSliderPercentageChanged;

        var pitchSlider = GetNode<HorizontalSlider>("BottomSection/PitchSlider");
        pitchSlider.PercentageChanged += OnPitchSliderPercentageChanged;

        base.Ready();
    }

    private void OnPitchSliderPercentageChanged(object? sender, float e)
    {
        Pitch = e * 2;
    }

    private void OnSliderReleased(object? sender, float e)
    {
        float timestamp = e * AudioLength;

        if (Playing)
        {
            Play(timestamp);
        }
        else
        {
            Seek(timestamp);
        }
    }

    private void OnVolumeSliderPercentageChanged(object? sender, float e)
    {
        Volume = e;
    }

    public override void Update()
    {
        //slider.MaxExternalValue = AudioLength;

        if (Playing)
        {
            slider.Percentage = TimePlayed / AudioLength;
        }

        //if (slider.Grabber != null)
        //{
        //if (!slider.Grabber.Pressed)
        //{
        //    slider.ExternalValue = TimePlayed;
        //
        //    // Calculate the percentage of the song played
        //    float percentage = TimePlayed / AudioLength;
        //
        //    slider.Percentage = percentage;
        //
        //    // Calculate the new X position of the middle button based on the slider's width
        //    float x = slider.Position.X + percentage * slider.Size.X;
        //    float y = slider.Grabber.GlobalPosition.Y;
        //
        //    // Update the middle button's position
        //    //slider.Grabber.GlobalPosition = new(x, y);
        //}
        //}

        base.Update();
    }
}