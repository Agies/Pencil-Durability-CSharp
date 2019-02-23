using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public void WriteOn(string text, ISurface surface)
        {
            if (text == null) return;
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