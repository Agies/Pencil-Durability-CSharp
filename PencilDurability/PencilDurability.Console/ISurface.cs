namespace PencilDurability.Console
{
    public interface ISurface
    {
        void Write(char text, uint? position = null);
    }
}