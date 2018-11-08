using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Uno {
    public class Card {
        
        private readonly ConsoleColor cardColor;
        private readonly int cardNum;
        
        private static ConsoleColor yellow = ConsoleColor.Yellow;
        private static ConsoleColor blue = ConsoleColor.Blue;
        private static ConsoleColor green = ConsoleColor.Green;
        private static ConsoleColor red = ConsoleColor.Red;
        private static ConsoleColor gray = ConsoleColor.Gray;

        private static IList<Card> playerHand = new List<Card>();
        private static IList<Card> compHand = new List<Card>();
        private static readonly Random rand = new Random();

        public Card (ConsoleColor cardColor, int cardNum) {
            this.cardColor = cardColor;
            this.cardNum = cardNum;
        }
        
        /// <summary>
        /// List of cards containing 21 of each color (yellow, blue, green, red)
        /// with each color having two of each rank (0-9) except for zero.
        /// Also includes cards of rank ten which are used to represent the draw
        /// two cards. The cards that are gray in color represent the wild and
        /// wild draw four cards.
        /// </summary>
        private static IList<Card> cards = new List<Card> {
            new Card(yellow, 0), new Card(blue, 0), new Card(green, 0), new Card(red, 0),
            new Card(yellow, 1), new Card(blue, 1), new Card(green, 1), new Card(red, 0),
            new Card(yellow, 1), new Card(blue, 1), new Card(green, 1), new Card(red, 1),
            new Card(yellow, 2), new Card(blue, 2), new Card(green, 2), new Card(red, 2),
            new Card(yellow, 2), new Card(blue, 2), new Card(green, 2), new Card(red, 2),
            new Card(yellow, 3), new Card(blue, 3), new Card(green, 3), new Card(red, 3),
            new Card(yellow, 3), new Card(blue, 3), new Card(green, 3), new Card(red, 3),
            new Card(yellow, 4), new Card(blue, 4), new Card(green, 4), new Card(red, 4),
            new Card(yellow, 4), new Card(blue, 4), new Card(green, 4), new Card(red, 4),
            new Card(yellow, 5), new Card(blue, 5), new Card(green, 5), new Card(red, 5),
            new Card(yellow, 5), new Card(blue, 5), new Card(green, 5), new Card(red, 5),
            new Card(yellow, 6), new Card(blue, 6), new Card(green, 6), new Card(red, 6),
            new Card(yellow, 6), new Card(blue, 6), new Card(green, 6), new Card(red, 6),
            new Card(yellow, 7), new Card(blue, 7), new Card(green, 7), new Card(red, 7),
            new Card(yellow, 7), new Card(blue, 7), new Card(green, 7), new Card(red, 7),
            new Card(yellow, 8), new Card(blue, 8), new Card(green, 8), new Card(red, 8),
            new Card(yellow, 8), new Card(blue, 8), new Card(green, 8), new Card(red, 8),
            new Card(yellow, 9), new Card(blue, 9), new Card(green, 9), new Card(red, 9),
            new Card(yellow, 9), new Card(blue, 9), new Card(green, 9), new Card(red, 9),
            
            new Card(yellow, 10), new Card(blue, 10), new Card(green, 10), new Card(red, 10),// Draw Two
            new Card(yellow, 10), new Card(blue, 10), new Card(green, 10), new Card(red, 10),
            
            new Card(gray, 11), new Card(gray, 11), new Card(gray, 11), new Card(gray, 11),// Wild Cards
            new Card(gray, 11), new Card(gray, 11), new Card(gray, 11), new Card(gray, 11),
            
            new Card(gray, 12), new Card(gray, 12), new Card(gray, 12), new Card(gray, 12),// Wild +4
            new Card(gray, 12), new Card(gray, 12), new Card(gray, 12), new Card(gray, 12)
        };

        public static void DistributeHands() {
            for (int i = 0; i < 7; i++) {
                
                int tempRand = rand.Next(cards.Count);
                Card tempCard = new Card(cards[tempRand].cardColor
                    , cards[tempRand].cardNum);
                cards.Remove(tempCard);
                playerHand.Add(tempCard);
                
                int tempRand2 = rand.Next(cards.Count);
                Card tempCard2 = new Card(cards[tempRand2].cardColor
                    , cards[tempRand2].cardNum);
                cards.Remove(tempCard2);
                compHand.Add(tempCard2);
            }
        }

        /// <summary>
        /// Method used to print a hand of cards.
        /// </summary>
        public static void printHands() {
            int cardCount = 1;
            if (playerHand.Count != 0) {
                for (int k = 0; k < playerHand.Count; k++) {
                    for (int i = 0; i <= 2; i++) {
                        Console.CursorLeft = 5;
                        for (int j = 0; j <= 3; j++) {
                            if (i == 0 && j == 0) {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.CursorLeft = 5;
                                if (playerHand[k].cardNum == 10) {
                                    Console.Write("+2");
                                    j++;
                                    continue; // Prevents card number being written.
                                }
                                if (playerHand[k].cardNum == 11) {
                                    Console.Write("W");
                                    continue;
                                }
                                if (playerHand[k].cardNum == 12) {
                                    Console.Write("W4");
                                    j += 2;
                                }
                                else {
                                    Console.Write(playerHand[k].cardNum);
                                    j++;
                                }
                            }
                            if (i == 2 && j == 3) {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(cardCount);
                                cardCount++;
                            }
                            else {
                                Console.BackgroundColor = playerHand[k].cardColor;
                                Console.Write(" ");
                            }
                        }
                        Console.CursorLeft = 5;
                        Console.WriteLine();
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }
        

        public static void PrintTitle(bool turn) {
            Console.CursorTop = 3;
            Console.CursorLeft = 20;
            Console.WriteLine("Welcome to Uno!");

            Console.CursorLeft = 20;
            if (turn == true) {
                Console.WriteLine("Player's Turn");
            } else {
                Console.WriteLine("Computer's Turn\n");
                turn = false;
            }
            Console.CursorLeft = 20;
            Console.WriteLine("Discard Pile");
        }

        public static void PrintCard(ConsoleColor color, int num) {
            for (int i = 0; i < 2; i++) {
                
            }
        }
    }
}