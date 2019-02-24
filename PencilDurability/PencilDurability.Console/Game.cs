using System.IO;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace PencilDurability.Console
{
    public class Game
    {
        private readonly TextWriter _output;
        private readonly TextReader _input;

        public Game(TextWriter output, TextReader input)
        {
            _output = output;
            _input = input;
        }

        public void Start()
        {
            _output.Write(Intro);
        }

        private const string Intro =
            "You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\n" +
            "What would you like to do?\n" +
            "1) Read the paper\n" +
            "2) Look at pencil";
    }
}