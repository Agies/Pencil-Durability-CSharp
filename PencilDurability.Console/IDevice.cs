namespace PencilDurability.Console
{
    public interface IDevice
    {
        string Examine();
        void WriteOn(string text, ISurface surface, int? startIndex = null);
        void EraseOn(string text, ISurface surface);
        void Sharpen();
    }
}