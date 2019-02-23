using System;
using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public uint Durability { get; private set; }

        public Pencil(uint durability = 100)
        {
            Durability = durability;
        }
        
        public void WriteOn(string text, ISurface surface)
        {
            if (surface == null) throw new NothingToWriteOnException();
            if (text == null) return;
            foreach (var c in text)
            {
                if (Durability <= 0)
                {
                    surface.Write(' ');
                }
                else
                {
                    surface.Write(c);
                }
                if (!char.IsWhiteSpace(c) && Durability > 0)
                {
                    Durability -= (char.IsUpper(c) ? 2u : 1u);
                }
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