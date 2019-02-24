using System.IO;
using System.Linq;

namespace PencilDurability.Console
{
    public class Game
    {
        private readonly TextWriter _output;
        private readonly TextReader _input;
        private readonly ISurface _surface;
        private readonly IDevice _device;

        public Game(TextWriter output, TextReader input, ISurface surface, IDevice device)
        {
            _output = output;
            _input = input;
            _surface = surface;
            _device = device;
        }

        public void Start()
        {
            var answer = "";
            var allowedAnswers = new string[] {"1", "2"};
            do
            {
                _output.WriteLine(Intro);
            } while (!allowedAnswers.Contains((answer = _input.ReadLine())));

            if (answer == "1")
            {
                _output.WriteLine(Reading);
            }
            else if (answer == "2")
            {
                _output.WriteLine(Examine);
            }
        }

        private string Examine =>
            $"You look at what you now understand to be a magic pencil, its stats are revealed in your mind.\n\n{_device.Examine()}";

        private const string Intro =
            "You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\n" +
            "What would you like to do?\n" +
            "1) Read the paper\n" +
            "2) Look at pencil";

        private string Reading =>
            $"You look at the simple sheet of paper and read the text written.\n\n{_surface.Show()}";
    }
}