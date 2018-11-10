using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Uno {
    public class Game {
        private static bool gameOver = false;
        private static int turn = 1;

        public static void GameLoop() {
            while (gameOver == false) {

                if (turn == 1) {
                    Card.PrintTitle(true);

                    Console.SetCursorPosition(6, 6);
                    Console.WriteLine("Player's Hand\n");
                    Card.printHands(Card.playerHand, 10);

                    Console.CursorTop = 8;
                    Card.printHands(Card.discardPile, 30);

                    Console.SetCursorPosition(45, 6);
                    Console.WriteLine("Computer's Hand");
                    Card.printHands(Card.compHand, 50);

                    int choice;
                    Console.SetCursorPosition(23, 15);
                    Console.WriteLine("Enter your choice: ");
                    Console.SetCursorPosition(27, 16);
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice < 1 || choice > Card.playerHand.Count()) {
                        Console.WriteLine("Enter a valid choice.");
                    }
                }
            }
        }
    }
}
