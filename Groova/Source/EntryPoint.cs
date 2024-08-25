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
            ClearColor = new(57, 57, 57, 255)
        };

        MainNode rootNode = new()
        {
            AudioPath = args.Length > 0 ?
                        args[0] :
                        "Assets/Audio.mp3"
        };

        Program program = new(windowData, rootNode, args);
        program.Run();
    }
}