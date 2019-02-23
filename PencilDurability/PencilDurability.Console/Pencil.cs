using System;
using System.Linq;
using System.Net.WebSockets;

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
            var startIndex = erasable.Show().LastIndexOf(text, StringComparison.Ordinal);
            if (startIndex < 0) return;
            var length = text.Length - 1;
            for (var i = length; i >= 0; i--)
            {
                if (EraserDurability == 0) return;
                var character = text[i];
                erasable.Erase(startIndex + i);
                EraserDurability -= char.IsWhiteSpace(character) ? 0u : 1u;
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