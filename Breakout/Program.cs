using System;
using DIKUArcade.Events;
using DIKUArcade.GUI;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {

            var windowArgs = new WindowArgs();
            var game = new Game(windowArgs);
            game.Run();
        }
    }
}
