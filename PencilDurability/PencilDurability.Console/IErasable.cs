namespace PencilDurability.Console
{
    public interface IErasable
    {
        void Erase(int position);
    }

    public interface IViewable
    {
        string Show();
    }
}