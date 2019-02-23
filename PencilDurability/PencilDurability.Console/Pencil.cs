using System;

namespace PencilDurability.Console
{
    public class Pencil
    {
        public uint Durability { get; private set; }
        public uint Length { get; private set; }

        public Pencil(uint durability = 100, uint length = 20)
        {
            Durability = durability;
            Length = length;
        }
        
        public void WriteOn(string text, ISurface surface)
        {
            if (surface == null) throw new NothingToWriteOnException();
            if (text == null) return;
            foreach (var c in text)
            {
                surface.Write(Durability <= 0 ? ' ' : c);
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