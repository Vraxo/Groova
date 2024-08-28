namespace Groova;

public partial class MainScene : Node
{
    public override void Build()
    {
        AddChild(new ClickManager());

        AddChild(new PlaylistsContainer());

        AddChild(new MusicPlayer
        {
            AutoPlay = false,
            Loop = true,
        });

        AddChild(new TopSection());

        AddChild(new BottomSection());
        
        //AddChild(new ItemList
        //{
        //    ItemSize = new(100, 40),
        //    OnUpdate = (list) =>
        //    {
        //        float x = list.Position.X;
        //        float y = 50;
        //        list.Position = new(x, y);
        //
        //
        //        float width = Raylib.GetScreenWidth();
        //        float height = Raylib.GetScreenHeight() - list.Position.Y - 80;
        //        list.Size = new(width, height);
        //    }
        //}, "PlaylistsList");

        //AddChild(new ItemList
        //{
        //    ItemSize = new(100, 40),
        //    OnUpdate = (list) =>
        //    {
        //        float x = list.Position.X;
        //        float y = 50;
        //        list.Position = new(x, y);
        //
        //        float width = Raylib.GetScreenWidth();
        //        float height = Raylib.GetScreenHeight() - list.Position.Y - 80;
        //        list.Size = new(width, height);
        //    }
        //}, "MusicsList");
    }
}