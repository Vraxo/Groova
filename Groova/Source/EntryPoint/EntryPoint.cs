namespace Groova;

public class EntryPoint
{
    [STAThread]
    public static void Main(string[] args)
    {
        WindowData windowData = new()
        {
            Title = "Groova",
            Resolution = new(360, 640),
            //ClearColor = new(57, 57, 57, 255)
            ClearColor = new(16, 16, 16, 255)
        };

        MainScene rootNode = new()
        {
        };

        Program program = new(windowData, rootNode, args);
        program.Run();
    }
}