namespace Groova;

public partial class MainScene : Node
{
    public override void Build()
    {
        //AddChild(new ClickManager());

        AddChild(new SongPlayer());

        AddChild(new TopSection());

        AddChild(new BottomSection());
    }
}