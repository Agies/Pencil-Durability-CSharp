using System;
using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public int Durability { get; }

        public Pencil()
        {
            Durability = 100;
        }
        
        public void WriteOn(string text, ISurface surface)
        {
            if (surface == null) throw new NothingToWriteOnException();
            if (text == null) return;
            foreach (var c in text)
            {
                surface.Write(c);
            }
        }

    }

    public class NothingToWriteOnException: Exception
    {
        public NothingToWriteOnException(): base("I have nothing on which to write!")
        {
            
        }
    }
}