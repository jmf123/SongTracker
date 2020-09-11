using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class SessionList
    {
        static string inputString;
        static int inputInt;
        static ConsoleCommand conCom = new ConsoleCommand();
        public SongMemory saveFile = new SongMemory();
        private List<Song> allSongs = new List<Song>();
        private List<Song> generatedList = new List<Song>();
        PlaylistGenerator generator;
        public void DisplaySongs(int listCount)
        {
            generator = new PlaylistGenerator(saveFile.GetSongs());
            generatedList = generator.GeneratePlaylist(listCount);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("New training session started!");
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                //END SESSION IF SONGS == 0
                if(generatedList.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Training session has run out of songs!");
                    Console.ForegroundColor = ConsoleColor.White;
                    conCom.AskInputKey("Press any key to continue...");
                    break;
                }
                //DISPLAY SONGS
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Displaying top songs sorted by priority:");
                Console.ForegroundColor = ConsoleColor.White;
                int i = 1;
                foreach(Song s in generatedList)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i:D2}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" = ");
                    s.sessionDisplay();
                    i++;
                }
                inputString = conCom.AskInputKey("\nA = Select song\nB = End session");
                //END SESSION
                if (inputString.Equals("B"))
                {
                    inputString = conCom.AskInputKey("Press Y to confirm End session");
                    if (inputString.Equals("Y"))
                    {
                        Console.WriteLine("\nEnd session called\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("");
                        continue;
                    }
                }
                //SELECT SONG
                if (inputString.Equals("A"))
                {
                    inputInt = conCom.AskInputNumber("Write down the song number and press Enter");
                    try
                    {
                        var selectedSong = generatedList[inputInt - 1];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"\n{ selectedSong.Name }");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" selected");
                        MarkSong(selectedSong);
                    }
                    catch
                    {
                        Console.WriteLine($"Not a valid song number!\n");
                    }
                }
            }
        }
        public void MarkSong(Song selectedSong)
        {
            var songCopy = selectedSong;
            inputString = conCom.AskInputKey("A = Mark new performance\nB = Cancel");
            //CANCEL SELECTED SONG
            if (inputString.Equals("B"))
            {
                return;
            }
            //MARK A NEW PERFORMANCE
            else if (inputString.Equals("A"))
            {
                Console.WriteLine("Great work!");
                inputInt = conCom.AskInputNumber("On a scale from 1 to 10, how well did it go?");
                    
                if (inputInt >= 1 && inputInt <= 10)  
                {  
                    selectedSong.LastPlayedPerformance = inputInt;
                    selectedSong.LastPlaytime = DateTime.Now;
                    Console.WriteLine($"You rated { selectedSong.Name } as { inputInt }/10!\n");
                    var editedList = saveFile.EditSong(saveFile.GetSongs(), selectedSong);
                    saveFile.SaveSongs(editedList);
                    generatedList.Remove(songCopy);
                }
                else
                {
                    Console.WriteLine($"Not a valid rating!\n");
                }
            }
        }
    }
}
