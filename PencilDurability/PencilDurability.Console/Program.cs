using System;

namespace PencilDurability.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game(System.Console.Out, System.Console.In, new Paper("She sells sea shells"), new Pencil());
        }
    }
}