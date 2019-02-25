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

        //NOTE: Should edit decrease durability? The story suggest that edit is a write, but does not directly state it. Additionally, would an @ be a capital letter?
        // Assuming there is durability loss the case then when out of durability the pencil would simply stop editing leaving overwritten characters untouched?
        // With confirmation, WriteOn and EditOn become a single method that takes in an option parameter of start position
        // https://github.com/Agies/Pencil-Durability-CSharp/tree/feature/editing_should_degrade
        public void EditOn<T>(int startIndex, string text, T surface) where T : ISurface
        {
            var existingText = surface.Show();
            for (var i = 0; i < text.Length; i++)
            {
                var pos = startIndex + i;
                var collisionCharacter = existingText[pos];
                surface.Write(char.IsWhiteSpace(collisionCharacter) ? text[i] : '@', pos);
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