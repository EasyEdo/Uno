using System;

namespace Uno{
    public class Program {
        static void Main(string[] args) {
            Random rand = new Random();
            Console.SetWindowSize(70, 40);

            Card.CreateCards();
            Card.DistributeHands();

            Game.GameLoop();
        }
    }
}
