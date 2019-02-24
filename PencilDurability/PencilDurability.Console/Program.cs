using System;

namespace PencilDurability.Console
{
    class Program
    {
        private static Game _game;

        static void Main(string[] args)
        {
            _game = new Game(System.Console.Out, System.Console.In, new Paper("She sells sea shells"), new Pencil());
            _game.Start();
        }
    }
}