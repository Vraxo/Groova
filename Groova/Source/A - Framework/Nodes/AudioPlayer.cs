using Raylib_cs;

namespace Groova;

public class AudioPlayer : Node
{
    public Music Audio { get; set; }
    public bool HasAudio = false;
    public bool AutoPlay { get; set; } = false;
    public bool Loop { get; set; } = false;
    public bool Playing => Raylib.IsMusicStreamPlaying(Audio);
    public float TimePlayed => Raylib.GetMusicTimePlayed(Audio);
    public float AudioLength => Raylib.GetMusicTimeLength(Audio);

    public float Volume
    {
        set
        {
            if (!HasAudio)
            {
                return;
            }

            Raylib.SetMusicVolume(Audio, value);
        }
    }

    public float Pitch
    {
        set
        {
            if (!HasAudio)
            {
                return;
            }

            Raylib.SetMusicPitch(Audio, value);
        }
    }

    public float Pan
    {
        set
        {
            if (!HasAudio)
            {
                return;
            }

            Raylib.SetMusicPan(Audio, value);
        }
    }

    public override void Ready()
    {
        if (!HasAudio)
        {
            return;
        }

        if (AutoPlay)
        {
            Play();
        }
    }

    public override void Update()
    {
        Raylib.UpdateMusicStream(Audio);

        if (TimePlayed >= AudioLength)
        {
            if (Loop)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Load(string path)
    {
        Audio = Raylib.LoadMusicStream(path);
        HasAudio = true;
    }

    public void Play(float timestamp = 0.1f)
    {
        if (!HasAudio)
        {
            return;
        }

        timestamp = Math.Clamp(timestamp, 0.1f, AudioLength);

        Raylib.PlayMusicStream(Audio);
        Raylib.SeekMusicStream(Audio, timestamp);
    }

    public void Resume()
    {
        if (!HasAudio)
        {
            return;
        }

        Raylib.ResumeMusicStream(Audio);
    }

    public void Pause()
    {
        if (!HasAudio)
        {
            return;
        }

        Raylib.PauseMusicStream(Audio);
    }

    public void Stop()
    {
        if (!HasAudio)
        {
            return;
        }

        Raylib.StopMusicStream(Audio);
    }

    public void Seek(float timestamp)
    {
        if (!HasAudio)
        {
            return;
        }

        timestamp = Math.Clamp(timestamp, 0.1f, AudioLength);

        Raylib.SeekMusicStream(Audio, timestamp);
    }
}