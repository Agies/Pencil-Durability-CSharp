using System;
using System.Linq;

namespace PencilDurability.Console
{
    public class Pencil
    {
        private readonly uint _initialDurability;
        public uint Durability { get; private set; }
        public uint Length { get; private set; }
        public uint EraserDurability { get; private set; }

        public Pencil(uint durability = 100, uint length = 20, uint eraserDurability = 20)
        {
            _initialDurability = Durability = durability;
            Length = length;
            EraserDurability = eraserDurability;
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

        public void Sharpen()
        {
            if (Length == 0) return;
            Durability = _initialDurability;
            Length--;
        }

        public void EraseOn<T>(string text, T erasable) where T: IErasable, IViewable
        {
            erasable.Erase(text);
            EraserDurability = (uint) (EraserDurability - text.Count(c => !char.IsWhiteSpace(c)));
        }
    }

    public class NothingToWriteOnException: Exception
    {
        public NothingToWriteOnException(): base("I have nothing on which to write!")
        {
            
        }
    }
}