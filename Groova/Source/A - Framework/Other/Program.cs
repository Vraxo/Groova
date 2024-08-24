using Raylib_cs;
using System.Reflection;

namespace Sonique;

public class Program(WindowData windowData, Node rootNode, string[] args)
{
    public Node RootNode = rootNode;
    public string[] Args = args;

    private readonly WindowData windowData = windowData;

    public void Run()
    {
        Initialize();
        RunLoop();
    }

    private void Initialize()
    {
        SetCurrentDirectory();

        int width = (int)windowData.Resolution.X;
        int height = (int)windowData.Resolution.Y;

        SetWindowFlags();

        Raylib.InitWindow(width, height, windowData.Title);
        Raylib.SetWindowMinSize(width, height);
        Raylib.InitAudioDevice();

        Raylib.SetWindowIcon(Raylib.LoadImage("Assets/Icon.png"));
        
        //Scene scene = new(rootNode);
        //var mainNode = scene.Instantiate<MainNode>();
        //RootNode = mainNode;
        RootNode.Program = this;

        RootNode.Start();
        RootNode.Build();
    }

    private void SetCurrentDirectory()
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;
        Environment.CurrentDirectory = Path.GetDirectoryName(assemblyLocation);
    }

    private static void SetWindowFlags()
    {
        Raylib.SetConfigFlags(
            //ConfigFlags.VSyncHint | 
            //ConfigFlags.Msaa4xHint |
            //ConfigFlags.HighDpiWindow |
            ConfigFlags.ResizableWindow |
            ConfigFlags.AlwaysRunWindow);
    }

    private void RunLoop()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
                Raylib.ClearBackground(windowData.ClearColor);
                RootNode.Process();
            Raylib.EndDrawing();

            PrintTree();
        }
    }

    private void PrintTree()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            Console.Clear();
            RootNode.PrintChildren();
        }
    }
}