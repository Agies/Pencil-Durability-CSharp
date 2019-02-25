namespace PencilDurability.Console
{
    public interface IDevice
    {
        string Examine();
        void WriteOn(string text, ISurface surface);
        void EraseOn(string text, ISurface surface);
        void EditOn(int position, string text, ISurface surface);
    }
}