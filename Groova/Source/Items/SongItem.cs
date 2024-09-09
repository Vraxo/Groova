namespace Groova;

public partial class SongItem : BaseItem
{
    public Playlist Playlist;
    public Song Song;

    private MainScene mainScene;
    private SongPlayer songPlayer;
    private CurrentSongDisplayer currentSongDisplayer;

    public override void Start()
    {
        Text = Path.GetFileNameWithoutExtension(Song.FilePath);
        base.Start();
        image.Load(Song.ImagePath);

        mainScene = GetNode<MainScene>("/root");
        songPlayer = GetNode<SongPlayer>("/root/SongPlayer");
        currentSongDisplayer = GetNode<CurrentSongDisplayer>("/root/BottomSection/CurrentSongDisplayer");
    }

    protected override void OnButtonLeftClicked(object? sender, EventArgs e)
    {
        songPlayer.Playlist = Playlist;
        songPlayer.LoadAndPlaySong(Song);

        mainScene.StopSearch(mainScene.Searching);

        currentSongDisplayer.SetSong(Song);
    }

    protected override void OnButtonRightClicked(object? sender, EventArgs e)
    {
        DeleteSongDialog dialog = new()
        {
            Playlist = Playlist,
            Song = Song
        };

        RootNode.AddChild(dialog);
    }

    protected override void SetImage(string imagePath)
    {
        PlaylistContainer.Instance.SetSongImage(Song, imagePath);
    }
}