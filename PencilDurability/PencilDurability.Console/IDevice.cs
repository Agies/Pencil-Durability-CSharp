namespace PencilDurability.Console
{
    public interface IDevice
    {
        string Examine();
        void WriteOn(string text, ISurface surface);
    }
}