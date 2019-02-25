using System;

namespace PencilDurability.Console
{
    public class Pencil: IDevice
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
        public void WriteOn(string text, ISurface surface, int? startIndex = null)
        {
            if (surface == null) throw new NothingToWriteOnException();
            if (text == null) return;
            
            var existingText = surface.Show();
            var start = startIndex ?? existingText.Length;
            for (var i = 0; i < text.Length; i++)
            {
                var pos = start + i;
                var collisionCharacter = pos < existingText.Length ? existingText[pos] : ' ';
                var outOfDurability = Durability <= 0;
                var toWrite = char.IsWhiteSpace(collisionCharacter) ? 
                    (outOfDurability ? ' ' : text[i]) : 
                    (outOfDurability ? collisionCharacter : '@');
                surface.Write(toWrite, pos);
                
                if (!char.IsWhiteSpace(toWrite) && !outOfDurability)
                {
                    Durability -= (char.IsUpper(toWrite) ? 2u : 1u);
                }
            }
        }

        public void Sharpen()
        {
            if (Length == 0) return;
            Durability = _initialDurability;
            Length--;
        }

        public void EraseOn(string text, ISurface surface)
        {
            var startIndex = surface.Show().LastIndexOf(text, StringComparison.Ordinal);
            if (startIndex < 0) return;
            var length = text.Length - 1;
            for (var i = length; i >= 0; i--)
            {
                if (EraserDurability == 0) return;
                var character = text[i];
                surface.Erase(startIndex + i);
                EraserDurability -= char.IsWhiteSpace(character) ? 0u : 1u;
            }
        }
        
        public string Examine()
        {
            return
                $"Stats:\n############################\nDurability: {Durability}\nLength: {Length}\nEraser Durability: {EraserDurability}\n############################";
        }
    }

    public class NothingToWriteOnException : Exception
    {
        public NothingToWriteOnException() : base("I have nothing on which to write!")
        {
        }
    }
}