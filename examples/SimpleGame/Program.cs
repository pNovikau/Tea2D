// See https://aka.ms/new-console-template for more information

//TODO: write my own UTF32 encoder
//TODO: figure out how to integrate with SFML or pick up another render library

namespace SimpleGame
{
    public class Program
    {
        public static void Main()
        {
            var game = new Game();

            game.Init();
            game.Run();
        }
    }
}