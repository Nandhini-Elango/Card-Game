using System;

namespace CardsApplication
{
    class Program
    {
        /// <summary>
        /// Play and Shuffle a card
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //initiates deck of cards
            CardGame cardGame = new CardGame();
            try
            {
                cardGame.StartAPlay(Convert.ToInt32(LoadEnum.InitLoad));
            }
            catch(StackOverflowException ex)
            {
                Console.WriteLine("Exception : " +  ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press Enter to restart or Escape to exit");
                if(Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    cardGame.StartAPlay(Convert.ToInt32(LoadEnum.InitLoad));
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
