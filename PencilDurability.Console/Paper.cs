using System.Text;

namespace PencilDurability.Console
{
    public class Paper: ISurface
    {
        private readonly StringBuilder _buffer;

        public Paper(string pretext = "")
        {
            _buffer = new StringBuilder(pretext);
        }
        
        public void Write(char text, int? position = null)
        {
            if (position != null)
            {
                var length = _buffer.Length - 1;
                if (position > length)
                {
                    _buffer.Append("".PadRight(position.Value - length));
                }

                _buffer[position.Value] = text;
            }
            else
            {
                _buffer.Append(text);
            }
        }

        //TODO: need to discuss with PO, what if a non-pencil tries to erase parts of the page without text
        //NOTE: The idea that erasing part of a page without text in real life and it would cause a reality crash is hilarious. Luckily pencils come with built in reality erasing paradox prevention systems.
        public void Erase(int position)
        {
            _buffer[position] = ' ';
        }

        public string Show()
        {
            return _buffer.ToString();
        }
    }
}