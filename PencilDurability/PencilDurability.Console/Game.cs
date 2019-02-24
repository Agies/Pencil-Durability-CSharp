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
            ExecuteIntro();
        }

        private void ExecuteIntro()
        {
            while (true)
            {
                var answer = "";
                var allowedAnswers = new[] {"1", "2", "Q"};
                do
                {
                    _output.WriteLine(Intro);
                } while (!allowedAnswers.Contains((answer = _input.ReadLine())));

                if (answer == "1")
                {
                    _output.WriteLine(Reading);
                    continue;
                }

                if (answer == "2")
                {
                    var examineResult = string.Format(Examine, _device.Examine());
                    _output.WriteLine(examineResult);
                    answer = _input.ReadLine();
                    if (answer == "4")
                    {
                        continue;
                    }
                    _output.WriteLine(Exit);
                }

                break;
            }
        }

        public const string Examine =
            "You look at what you now understand to be a magic pencil, its stats are revealed in your mind.\n\n{0}\n\n" +
            "What would you like to do?\n" +
            "1) Write\n" +
            "2) Erase\n" +
            "3) Sharpen\n" +
            "4) Look at the desk\n" +
            "Q) Quit";

        public const string Intro =
            "You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\n" +
            "What would you like to do?\n" +
            "1) Read the paper\n" +
            "2) Look at pencil\n" +
            "Q) Quit";

        private string Reading =>
            $"You look at the simple sheet of paper and read the text written.\n\n{_surface.Show()}";

        public const string Exit = "Tank you for playing";
    }
}