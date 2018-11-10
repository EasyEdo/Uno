using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Uno {
    public struct Card {
        
        private readonly ConsoleColor cardColor;
        private readonly int cardNum;
        
        private static ConsoleColor yellow = ConsoleColor.Yellow;
        private static ConsoleColor blue = ConsoleColor.Blue;
        private static ConsoleColor green = ConsoleColor.Green;
        private static ConsoleColor red = ConsoleColor.Red;
        private static ConsoleColor gray = ConsoleColor.Gray;

        private static IList<Card> cards = new List<Card>();
        public static IList<Card> playerHand = new List<Card>();
        public static IList<Card> compHand = new List<Card>();
        public static IList<Card> discardPile = new List<Card>();

        private static readonly Random rand = new Random();

        public Card (ConsoleColor cardColor, int cardNum) {
            this.cardColor = cardColor;
            this.cardNum = cardNum;
        }

        /// <summary>
        /// Adds cards to the List of cards containing 21 of each color 
        /// (yellow, blue, green, red) with each color having two of each rank (0-9)
        /// except for zero. Also includes cards of rank ten which are used to 
        /// represent the draw two cards. The cards that are gray in color represent
        /// the wild and wild draw four cards.
        /// </summary>
        public static void CreateCards() {
            // Zero Cards
            cards.Add(new Card(yellow,   0));
            cards.Add(new Card(blue,     0));
            cards.Add(new Card(green,    0));
            cards.Add(new Card(red,      0));

            // Normal Cards
            for (int i = 1; i <= 10; i++) {
                for (int j = 0; j < 2; j++) {
                    cards.Add(new Card(yellow,   i));
                    cards.Add(new Card(blue,     i));
                    cards.Add(new Card(green,    i));
                    cards.Add(new Card(red,      i));
                }
            }

            // Wild Cards
            for (int i = 0; i < 8; i++) {
                cards.Add(new Card(gray, 11));
            }

            // Wild +4
            for (int i = 0; i < 8; i++) {
                cards.Add(new Card(gray, 12));
            }
        }

        /// <summary>
        /// Randomly adds cards from the card list to the players hand and
        /// to the computers hand.
        /// </summary>
        public static void DistributeHands() {
            for (int i = 0; i < 7; i++) {
                // Adding to Players Hand.
                int tempRand = rand.Next(cards.Count);
                Card tempCard = new Card(cards[tempRand].cardColor
                    , cards[tempRand].cardNum);
                cards.Remove(tempCard);
                playerHand.Add(tempCard);

                // Adding to Computers Hand.
                int tempRand2 = rand.Next(cards.Count);
                Card tempCard2 = new Card(cards[tempRand2].cardColor
                    , cards[tempRand2].cardNum);
                cards.Remove(tempCard2);
                compHand.Add(tempCard2);
            }

            // Adding initial card to discard pile.
            int tempRand3 = rand.Next(cards.Count);
            Card tempCard3 = new Card(cards[tempRand3].cardColor
                , cards[tempRand3].cardNum);
            cards.Remove(tempCard3);
            discardPile.Add(tempCard3);
        }

        /// <summary>
        /// Method used to print a hand of cards. Can also be used
        /// to print the discard pile.
        /// </summary>
        public static void printHands(IList<Card> hand, int cursor) {
            int cardCount = 1;
            if (hand.Count != 0) {
                for (int k = 0; k < hand.Count; k++) {
                    for (int i = 0; i <= 2; i++) {
                        Console.CursorLeft = cursor;
                        for (int j = 0; j <= 3; j++) {
                            if (i == 0 && j == 0) {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.CursorLeft = cursor;
                                if (hand[k].cardNum == 10) {
                                    Console.Write("+2");
                                    j++;
                                    continue; // Prevents card number being written.
                                }
                                if (hand[k].cardNum == 11) {
                                    Console.Write("W");
                                    continue;
                                }
                                if (hand[k].cardNum == 12) {
                                    Console.Write("W4");
                                    j += 2;
                                }
                                else {
                                    Console.Write(hand[k].cardNum);
                                    j++;
                                }
                            }
                            if (i == 2 && j == 3) {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(cardCount);
                                cardCount++;
                            }
                            else {
                                Console.BackgroundColor = hand[k].cardColor;
                                Console.Write(" ");
                            }
                        }
                        Console.CursorLeft = cursor;
                        Console.WriteLine();
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }
        
        /// <summary>
        /// Prints a title based off of the player and computers turn.
        /// </summary>
        /// <param name="turn"></param>
        public static void PrintTitle(bool turn) {
            Console.CursorTop = 3;
            Console.CursorLeft = 26;
            Console.WriteLine("Welcome to Uno!");

            Console.CursorLeft = 2;
            if (turn == true) {
                Console.WriteLine("Player's Turn, Choose a card to place or enter to draw and pass.\n");
            } else {
                Console.WriteLine("Computer's Turn, please wait.\n");
                turn = false;
            }
            Console.CursorLeft = 26;
            Console.WriteLine("Discard Pile");
        }
    }
}