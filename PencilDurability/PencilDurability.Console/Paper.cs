using System;
using System.Text;

namespace PencilDurability.Console
{
    public class Paper: ISurface, IErasable, IViewable, IEditable
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
                if (position > _buffer.Length)
                {
                    _buffer.Append("".PadRight(position.Value - (_buffer.Length)));
                }
            }

            _buffer.Append(text);
        }

        //TODO: need to discuss with PO, what if a non-pencil tries to erase parts of the page without text
        //NOTE: The idea that erasing part of a page without text in real life would cause a reality crash is hilarious, luckily pencils come with built in reality erasing paradox prevention systems.
        public void Erase(int position)
        {
            _buffer[position] = ' ';
        }

        public string Show()
        {
            return _buffer.ToString();
        }

        public void Replace(char text, int position)
        {
            if (position > _buffer.Length)
            {
                _buffer.Append("".PadRight(position - (_buffer.Length - 1)));
            }
            _buffer[position] = text;
        }
    }
}