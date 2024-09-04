namespace Groova;

public class Song
{
    public string FilePath { get; set; }
    public string ImagePath { get; set; }

    public Song() { }

    public Song(string filePath, string imagePath = null)
    {
        FilePath = filePath;
        ImagePath = imagePath;
    }

    public string GetName()
    {
        return Path.GetFileNameWithoutExtension(FilePath);
    }
}
