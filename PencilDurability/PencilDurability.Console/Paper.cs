using System;
using System.Text;

namespace PencilDurability.Console
{
    public class Paper: ISurface, IErasable
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

        public void Erase(string text)
        {
            if (text == string.Empty) return;
            _buffer.Replace(text, "".PadRight(text.Length), _buffer.ToString().LastIndexOf(text, StringComparison.Ordinal), text.Length);
        }

        public string Show()
        {
            return _buffer.ToString();
        }
    }
}