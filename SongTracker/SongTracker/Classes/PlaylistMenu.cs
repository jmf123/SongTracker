using System;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class PlaylistMenu
    {
        static string inputString;
        static int inputInt;
        static ConsoleCommand conCom = new ConsoleCommand();
        public static void StartMainMenu()
        {
            Console.WindowWidth = 110;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SongTracker ConsoleApp!");
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                //DISPLAY MAIN MENU
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Main Menu:");
                Console.ForegroundColor = ConsoleColor.White;
                inputString = conCom.AskInputKey("A = Start session\nS = Edit songs\nB = End program");
                //END PROGRAM
                if (inputString.Equals("B"))
                {
                    inputString = conCom.AskInputKey("Press Y to confirm End program");
                    if (inputString.Equals("Y"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("");
                        continue;
                    }
                }
                switch (inputString)
                {
                    //START SESSION
                    case "A":
                        StartSession();
                        break;
                    //SONG EDITOR
                    case "S":
                        StartSongEditor();
                        break;
                }
            }
        }
        public static void StartSession()
        {
            inputInt = conCom.AskInputNumber("Write down the number of songs you want in your playlist and press Enter");
            //CREATE SESSION
            if (!(inputInt <= 0))
            {
                var traingSession = new SessionList();
                traingSession.DisplaySongs(inputInt);
            }
            else
            {
                Console.WriteLine($"Not a valid number!\n");
            }
        }
        public static void StartSongEditor()
        {
            //CREATE EDITOR
            Console.WriteLine("Song editor called...\n");
            var songEditor = new SongEditorList();
            songEditor.DisplaySongs();
        }
    }
}
