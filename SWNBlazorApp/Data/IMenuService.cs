namespace SWNBlazorApp.Data;

public interface IMenuService
{
    event EventHandler<EventArgs> OnChanged;
    void NotifyChanged();
}

public class MenuService : IMenuService
{
    public event EventHandler<EventArgs>? OnChanged;

    public void NotifyChanged()
    {
        OnChanged?.Invoke(this, EventArgs.Empty);
    }
}