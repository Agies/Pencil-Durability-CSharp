using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public void WriteOn(string text, ISurface surface)
        {
            surface.Write(text.First());
        }
    }
    
    public interface ISurface
    {
        void Write(char text);
    }
}