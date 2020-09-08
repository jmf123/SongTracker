using System;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class PlaylistMenu
    {
        public static void StartMainMenu()
        {
            Console.WindowWidth = 110;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SongTracker ConsoleApp!");
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Main Menu:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("A = Start session\nS = Edit songs\nB = End program");
                char input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);
                Console.WriteLine("");
                if (input.Equals('B'))
                {
                    Console.WriteLine("Press Y to confirm End program");
                    char endInput = Console.ReadKey().KeyChar;
                    endInput = char.ToUpper(endInput);
                    if (endInput.Equals('Y'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("");
                        continue;
                    }
                }
                switch (input)
                {
                    case 'A':
                        StartSession();
                        break;
                    case 'S':
                        StartSongEditor();
                        break;
                }
            }
        }
        public static void StartSession()
        {
            Console.WriteLine("Write down the number of songs you want in your playlist and press Enter");
            string input = Console.ReadLine();
            try {
                int number = int.Parse(input.ToString());
                if(!(number <= 0))
                {
                    var traingSession = new SessionList();
                    traingSession.DisplaySongs(number);
                }
                else
                {
                    Console.WriteLine($"{ input } is not a valid number!\n");
                }
            } catch {
                Console.WriteLine($"{ input } in not a valid number!\n");
            }
        }
        public static void StartSongEditor()
        {
            Console.WriteLine("Song editor called...\n");
            var songEditor = new SongEditorList();
            songEditor.DisplaySongs();
        }
    }
}
