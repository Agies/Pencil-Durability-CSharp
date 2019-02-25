using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace PencilDurability.Console
{
    public class Game
    {
        private readonly TextWriter _output;
        private readonly TextReader _input;
        private readonly ISurface _surface;
        private readonly IDevice _device;

        private const string QuitCommands = "qQ";

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
                string answer;
                var allowedAnswers = new[] {"1", "2", "Q", "q"};
                do
                {
                    _output.WriteLine(Intro);
                } while (!allowedAnswers.Contains((answer = _input.ReadLine())));

                if (answer == "1")
                {
                    var readingResult = string.Format(Reading, _surface.Show());
                    _output.WriteLine(readingResult);
                    continue;
                }

                if (answer == "2")
                {
                    if (ExaminePath() == Flow.Continue)
                    {
                        continue;
                    }
                }

                break;
            }

            _output.WriteLine(Exit);
        }

        private Flow ExaminePath()
        {
            while (true)
            {
                var examineResult = string.Format(Examine, _device.Examine());
                _output.WriteLine(examineResult);
                var answer = _input.ReadLine() ?? "";
                if (answer.StartsWith("write ", true, CultureInfo.InvariantCulture))
                {
                    _device.WriteOn(answer.Substring(6), _surface);
                }

                if (answer.StartsWith("erase ", true, CultureInfo.InvariantCulture))
                {
                    _device.EraseOn(answer.Substring(6), _surface);
                }

                if (answer.StartsWith("edit ", true, CultureInfo.InvariantCulture))
                {
                    var split = answer.Split();
                    if (split.Length < 3 || !int.TryParse(split[1], out var position))
                    {
                        return Flow.Continue;
                    }
                    _device.EditOn(position, string.Join(' ', split.Skip(2)), _surface);
                }

                if (answer == "2")
                {
                    return Flow.Continue;
                }

                if (QuitCommands.Contains(answer))
                {
                    return Flow.Break;
                }
            }
        }

        enum Flow
        {
            Continue,
            Break
        }

        public const string Examine =
            "You look at what you now understand to be a magic pencil, its stats are revealed in your mind.\n\n{0}\n\n" +
            "What would you like to do?\n" +
            "Type Write [Words]\n" +
            "Type Erase [Word]\n" +
            "Type Edit [Position] [Words]\n" +
            "1) Sharpen\n" +
            "2) Look at the desk\n" +
            "Q) Quit";

        public const string Intro =
            "You see a simple sheet of paper sitting on a desk. A pencil sits across the paper. There appear to be words written on the paper.\n" +
            "What would you like to do?\n" +
            "1) Read the paper\n" +
            "2) Look at pencil\n" +
            "Q) Quit";

        public const string Reading = "You look at the simple sheet of paper and read the text written.\n\n{0}_\n";

        public const string Exit = "Thank you for playing";
    }
}