using System;
using System.Text;

namespace PencilDurability.Console
{
    public class Paper: ISurface, IErasable, IViewable
    {
        private readonly StringBuilder _buffer;

        public Paper(string pretext = "")
        {
            _buffer = new StringBuilder(pretext);
        }
        
        public void Write(char text)
        {
            _buffer.Append(text);
        }

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