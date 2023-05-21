using System;
//using System.Threading;
namespace StarterGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.Start();
            game.Play();
            game.End();
        }
    }
}