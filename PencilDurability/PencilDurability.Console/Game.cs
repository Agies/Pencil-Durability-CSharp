using System.IO;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace PencilDurability.Console
{
    public class Game
    {
        private readonly TextWriter _output;
        private readonly TextReader _input;
        private readonly ISurface _surface;

        public Game(TextWriter output, TextReader input, ISurface surface)
        {
            _output = output;
            _input = input;
            _surface = surface;
        }

        public void Start()
        {
            _output.Write(Intro);
            var answer = _input.ReadLine();
            if (answer == "1")
            {
                _output.Write($"{Reading}\n\n{_surface.Show()}");
            }
        }

        private const string Intro =
            "You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\n" +
            "What would you like to do?\n" +
            "1) Read the paper\n" +
            "2) Look at pencil";

        private const string Reading =
            "You look at the simple sheet of paper and read the text written.";
    }
}