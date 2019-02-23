namespace PencilDurability.Console
{
    public interface IErasable
    {
        void Erase(string text);
    }

    public interface IViewable
    {
        string Show();
    }
}