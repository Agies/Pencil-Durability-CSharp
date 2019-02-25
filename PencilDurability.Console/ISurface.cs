namespace PencilDurability.Console
{
    public interface ISurface
    {
        void Write(char text, int? position = null);
        void Erase(int position);
        string Show();
    }
}