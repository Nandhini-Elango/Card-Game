using System;
using System.Collections.Generic;

namespace CardsApplication
{
    class CardGame
    {
        #region Fields
        private static Stack<string> cardStack = new Stack<string>();
        #endregion

        #region Constructor
        public CardGame()
        {
            DeckOfCards(); //cards stored in stack during initializing an object
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Provides option for start the play and initiates by input
        /// </summary>
        /// <param name="loadValue"></param>
        internal void StartAPlay(int loadValue)
        {
            if (loadValue == 0)
            {
                Console.WriteLine();
                Console.WriteLine("**********");
                Console.WriteLine("CARD GAME");
                Console.WriteLine("**********");
            }
            Console.WriteLine();
            Console.WriteLine("Press any of the keys to play");
            Console.WriteLine("DownArrow - Play a card");
            Console.WriteLine("Spacebar - Shuffle the deck");
            if (loadValue != 0)
                Console.WriteLine("Backspace - Restart the game");
            Console.WriteLine("---------------*****---------------");
            Console.Write("Your option : ");

            var option = Console.ReadKey().Key;
            if (option != 0)
            {
                OptionControls(option, loadValue);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Controls over selected options
        /// </summary>
        /// <param name="option"></param>
        /// <param name="loadValue"></param>
        private void OptionControls(ConsoleKey option, int loadValue)
        {
            bool doDefault = false;
            switch (option)
            {
                case ConsoleKey.DownArrow:
                    Console.WriteLine("Play");
                    Console.Write("---------------*****---------------");
                    PlayACard();
                    StartAPlay(Convert.ToInt32(LoadEnum.InitLoad));
                    break;
                case ConsoleKey.Spacebar:
                    Console.WriteLine("Shuffle");
                    Console.Write("---------------*****---------------");
                    ShuffleDeck();
                    Console.WriteLine("Shuffled the Cards");
                    StartAPlay(Convert.ToInt32(LoadEnum.Reload));
                    break;
                case ConsoleKey.Backspace:
                    if (loadValue != 0) // not to show Restart option at start of the game
                    {
                        Console.WriteLine("Restart");
                        Console.Write("---------------*****---------------");
                        Restart();
                        Console.WriteLine("Game Restarts");
                        StartAPlay(Convert.ToInt32(LoadEnum.InitLoad));
                    }
                    else
                    {
                        doDefault = true;
                    }
                    break;
                default:
                    doDefault = true;
                    break;
            }
            if (doDefault) // Keys pressed other than in option
            {
                Console.WriteLine();
                Console.Write("---------------*****---------------");
                Console.WriteLine("Invalid option. Try Again!");
                StartAPlay(Convert.ToInt32(LoadEnum.Reload));
            }
        }
        /// <summary>
        /// Load deck of cards into stack
        /// </summary>
        private void DeckOfCards()
        {
            //store the deck of cards
            string[] symbols = { "Clubs", "Diamonds", "Spades", "Hearts" };
            string[] cardNumbers = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            cardStack.Clear();
            for (int sym = 0; sym < symbols.Length; sym++)
            {
                for (int cn = 0; cn < cardNumbers.Length; cn++)
                {
                    cardStack.Push(symbols[sym] + " " + cardNumbers[cn]);
                }
            }
            ShuffleDeck(); // shuffle the stored cards
        }
        /// <summary>
        /// Playing a card and restarting once game is over
        /// </summary>
        private void PlayACard()
        {
            if (cardStack.Count != 0)  // check for cards in stack to show
            {
                Console.WriteLine("Your card : " + cardStack.Peek());  // show card at top of stack
                cardStack.Pop();
                if (cardStack.Count != 0)  // only provide option for play, if there are any cards in stack
                    StartAPlay(Convert.ToInt32(LoadEnum.Reload));
                else
                    PlayACard();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red; // Game over to highlight in red
                Console.WriteLine("****GAME OVER****");
                Console.ResetColor(); // Resets color after game over
                Console.Beep(); // Highlight game over with beep
                Console.WriteLine("Do you want to restart the game ?"); // option for restart or exit
                Console.WriteLine("A - Yes, Continue || B - No, Exit");
                Console.WriteLine("---------------*****---------------");
                Console.Write("Your option : ");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.A:
                        Console.WriteLine();
                        Console.Write("---------------*****---------------");
                        DeckOfCards();
                        break;
                    case ConsoleKey.B:
                        Console.WriteLine();
                        Console.Write("---------------*****---------------");
                        Environment.Exit(0);
                        break;
                    default:   // Handled invalid option
                        Console.WriteLine();
                        Console.Write("---------------*****---------------");
                        Console.WriteLine("Invalid option. Try Again!");
                        PlayACard();
                        break;
                }
            }
        }
        /// <summary>
        /// Shuffle the deck of cards
        /// </summary>
        private void ShuffleDeck()
        {
            var cardsValues = cardStack.ToArray();
            // take random values from stack and swap with values in stack
            for (int card = 0; card < cardStack.Count;  card++)
            {
                int randomCard = new Random().Next(card, cardStack.Count-1);
                string temp = cardsValues[card];
                cardsValues[card] = cardsValues[randomCard];
                cardsValues[randomCard] = temp;
            }
            cardStack.Clear();  // clearing stack for storing randomly stored values
            foreach (var value in cardsValues)
            {
                cardStack.Push(value);  // storing random values in stack
            }
        }
        /// <summary>
        /// Restart the game
        /// </summary>
        private void Restart()
        {
            DeckOfCards();  // filling shuffled deck of cards again on clearing stack
        }
        #endregion
    }
}
