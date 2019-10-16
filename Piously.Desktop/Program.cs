using System;
using Piously.Game;

namespace Piously.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            PiouslyGame game = new PiouslyGame(5);
            Console.WriteLine(game.getY());
        }
    }
}
