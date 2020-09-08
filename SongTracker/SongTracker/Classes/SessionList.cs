using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class SessionList
    {
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
                if(generatedList.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Training session has run out of songs!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine("");
                    break;
                }
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
                Console.WriteLine("\nA = Select song\nB = End session");
                char input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);
                Console.WriteLine("");
                if (input.Equals('B'))
                {
                    Console.WriteLine("Press Y to confirm End session");
                    char endInput = Console.ReadKey().KeyChar;
                    endInput = char.ToUpper(endInput);
                    if (endInput.Equals('Y'))
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
                if (input.Equals('A'))
                {
                    Console.WriteLine("Write down the song number and press Enter");
                    string selectedSongNumber = Console.ReadLine();
                    try
                    {
                        int number = int.Parse(selectedSongNumber.ToString());
                        var selectedSong = generatedList[number - 1];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"\n{ selectedSong.Name }");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" selected");
                        MarkSong(selectedSong);
                    }
                    catch
                    {
                        Console.WriteLine($"{ selectedSongNumber } in not a valid number!\n");
                    }
                }
            }
        }
        public void MarkSong(Song selectedSong)
        {
            var songCopy = selectedSong;
            Console.WriteLine("A = Mark new performance\nB = Cancel");
            char input = Console.ReadKey().KeyChar;
            input = char.ToUpper(input);
            Console.WriteLine("");
            if (input.Equals('B'))
            {
                return;
            }
            else if (input.Equals('A'))
            {
                Console.WriteLine("Great work!");
                Console.WriteLine("On a scale from 1 to 10, how well did it go?");
                string performanceInput = Console.ReadLine();
                try
                {
                    int number = int.Parse(performanceInput.ToString());
                    if (number >= 1 && number <= 10)
                    {
                        selectedSong.LastPlayedPerformance = number;
                        selectedSong.LastPlaytime = DateTime.Now;
                        Console.WriteLine($"You rated { selectedSong.Name } as { number }/10!\n");
                        var editedList = saveFile.EditSong(saveFile.GetSongs(), selectedSong);
                        saveFile.SaveSongs(editedList);
                        generatedList.Remove(songCopy);
                    }
                    else
                    {
                        Console.WriteLine($"{ number } in not a valid rating!\n");
                    }
                }
                catch
                {
                    Console.WriteLine($"{ input } in not a valid number!\n");
                }
            }
        }
    }
}
