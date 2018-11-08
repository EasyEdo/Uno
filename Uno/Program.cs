using System;

namespace Uno{
    public class Program {
        static void Main(string[] args) {
            Random rand = new Random();
            
            Card.DistributeHands();
            Card.printHands();
            //Card.PrintTitle(true);
        }
    }
}
