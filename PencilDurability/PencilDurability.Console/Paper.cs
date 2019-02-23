using System.Text;

namespace PencilDurability.Console
{
    public class Paper: ISurface
    {
        private readonly StringBuilder _buffer = new StringBuilder("She sells sea shells");
        
        public void Write(char text)
        {
            _buffer.Append(text);
        }

        public string Show()
        {
            return _buffer.ToString();
        }
    }
}