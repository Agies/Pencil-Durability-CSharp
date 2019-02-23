using System;
using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public int Durability { get; private set; }

        public Pencil(int durability = 100)
        {
            Durability = durability;
        }
        
        public void WriteOn(string text, ISurface surface)
        {
            if (surface == null) throw new NothingToWriteOnException();
            if (text == null) return;
            foreach (var c in text)
            {
                if (!char.IsWhiteSpace(c))
                {
                    Durability -= (char.IsUpper(c) ? 2 : 1);
                }

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