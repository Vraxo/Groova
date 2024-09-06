namespace Groova;

public abstract class BaseDeleteDialog : ConfirmDialog
{
    protected abstract string ItemName { get; }

    public override void Start()
    {
        SetLabelText();
        base.Start();
    }

    protected void SetLabelText()
    {
        string itemName = Path.GetFileNameWithoutExtension(ItemName);
        string truncatedItemName = TruncateItemName(itemName);

        GetNode<Label>("Message").Text = $"Delete '{truncatedItemName}'?";
    }

    private static string TruncateItemName(string itemName)
    {
        if (itemName.Length > 25)
        {
            return itemName.Substring(0, 22) + "...";
        }

        return itemName;
    }
}