using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public void WriteOn(string text, ISurface surface)
        {
            foreach (var c in text)
            {
                surface.Write(c);
            }
        }
    }
    
    public interface ISurface
    {
        void Write(char text);
    }
}