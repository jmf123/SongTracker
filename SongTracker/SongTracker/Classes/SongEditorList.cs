using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SongTracker.Classes
{
    class SongEditorList
    {
        public SongMemory saveFile = new SongMemory();
        private List<Song> allSongs = new List<Song>();
        public void DisplaySongs()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Song Editor Menu:");
                Console.WriteLine("Displaying all available songs:");
                Console.ForegroundColor = ConsoleColor.White;
                allSongs = saveFile.GetSongs();
                allSongs.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
                int i = 1;
                foreach (Song s in allSongs)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i:D2}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" = ");
                    s.editorDisplay();
                    i++;
                }
                Console.WriteLine("");
                Console.WriteLine("A = Add new song\nS = Select song\nB = End editor");
                char input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);
                Console.WriteLine("");
                if (input.Equals('B'))
                {
                    Console.WriteLine("Press Y to confirm End editor");
                    char endInput = Console.ReadKey().KeyChar;
                    endInput = char.ToUpper(endInput);
                    if (endInput.Equals('Y'))
                    {
                        Console.WriteLine("");
                        Console.WriteLine("End editor called");
                        Console.WriteLine("");
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
                    Console.WriteLine("Adding new song...");
                    CreateSong();
                }
                if (input.Equals('S'))
                {
                    Console.WriteLine("Write down a song number and press Enter");
                    string selectedSongNumber = Console.ReadLine();
                    try
                    {
                        int number = int.Parse(selectedSongNumber.ToString());
                        var selectedSong = allSongs[number - 1];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{ selectedSong.Name }");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" selected");
                        Console.WriteLine("A = Edit song\nD = Delete song\nB = Cancel\n");
                        char editInput = Console.ReadKey().KeyChar;
                        editInput = char.ToUpper(editInput);
                        Console.WriteLine("");
                        if (editInput.Equals('A'))
                        {
                            EditSong(selectedSong);
                        }
                        if (editInput.Equals('D'))
                        {
                            Console.WriteLine("Press Y to confirm deletion");
                            char endInput = Console.ReadKey().KeyChar;
                            endInput = char.ToUpper(endInput);
                            if (endInput.Equals('Y'))
                            {
                                var deletedList = saveFile.DeleteSong(allSongs, selectedSong);
                                saveFile.SaveSongs(deletedList);
                                Console.WriteLine("");
                                Console.WriteLine("Song deleted!");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("");
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"{ selectedSongNumber } in not a valid number!\n");
                    }
                }
            }
        }
        public void CreateSong()
        {
            var newSong = new Song();
            Console.WriteLine("Write down a new song name and press Enter\n(Empty => cancel)");
            string newName = Console.ReadLine();
            if (!newName.Equals(""))
            {
                newSong.Name = newName;
                if (saveFile.songExists(saveFile.GetSongs(), newSong))
                {
                    Console.WriteLine($"Add new song cancelled. { newSong.Name } already exists!\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine($"{ newName } set to new name!\n");
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            DateTime newLastPlayed;
            Console.WriteLine("Write down last playdate in 'DD.MM.YYYY' format and press Enter\n(Empty or invalid => cancel)");
            string newPlaytime = Console.ReadLine();
            if (!newPlaytime.Equals(""))
            {
                try
                {
                    newLastPlayed = DateTime.ParseExact(newPlaytime, "dd.MM.yyyy", null);
                    if ((DateTime.Now - newLastPlayed).TotalDays < 0)
                    {
                        Console.WriteLine($"Add last playdate cancelled, { newPlaytime } is invalid.\n");
                        return;
                    }
                    newSong.LastPlaytime = newLastPlayed;
                    Console.WriteLine($"{ newLastPlayed:dd/MM/yyyy} set to last playdate!\n");
                }
                catch
                {
                    Console.WriteLine($"Add last playdate cancelled, { newPlaytime } is invalid.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            Console.WriteLine("Write down a song priority from 1 - 10 and press Enter\n(Empty or invalid => cancel)");
            string newPriority = Console.ReadLine();
            if (!newPriority.Equals(""))
            {
                try
                {
                    int number = int.Parse(newPriority.ToString());
                    if (number < 0 || number > 10)
                    {
                        Console.WriteLine($"Add new song cancelled, { newPriority } is invalid.\n");
                        return;
                    }
                    else
                    {
                        newSong.Priority = number;
                        Console.WriteLine($"{ newPriority } set to new priority!\n");
                    }
                }
                catch
                {
                    Console.WriteLine($"Add new song cancelled, { newPriority } is invalid.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            Console.WriteLine("Write down your latest song performance from 1 - 10 and press Enter\n(Empty or invalid => cancel)");
            string newPerformance = Console.ReadLine();
            if (!newPerformance.Equals(""))
            {
                try
                {
                    int number = int.Parse(newPerformance.ToString());
                    if (number < 0 || number > 10)
                    {
                        Console.WriteLine($"Add new song cancelled, { newPerformance } is invalid.\n");
                        return;
                    }
                    else
                    {
                        newSong.LastPlayedPerformance = number;
                        Console.WriteLine($"{ newPerformance } set to new latest performance!\n");
                    }
                }
                catch
                {
                    Console.WriteLine($"Add new song cancelled, { newPerformance } is invalid.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            Console.WriteLine("Add new song completed.\n");
            var savedList = saveFile.AddSong(saveFile.GetSongs(), newSong);
            saveFile.SaveSongs(savedList);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine("");
        }
        public void EditSong(Song selectedSong)
        {
            var booleanSong = new Song();
            var songCopy = selectedSong;
            Console.WriteLine($"Editing { songCopy.Name }");
            Console.WriteLine("Write down new song name and press Enter\n(Empty => skip)");
            bool newNameEdited = false;
            string newName = Console.ReadLine();
            if (!newName.Equals(""))
            {
                booleanSong.Name = newName;
                if (saveFile.songExists(saveFile.GetSongs(), booleanSong) && (selectedSong.Name != booleanSong.Name))
                {
                    Console.WriteLine($"Song name { songCopy.Name } already exists! Cancelling...");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                songCopy.Name = newName;
                Console.WriteLine($"{ newName } set to new name!\n");
                newNameEdited = true;
            }
            else
            {
                newNameEdited = false;
                Console.WriteLine("Name edit skipped\n");
            }
            DateTime newLastPlayed;
            Console.WriteLine("Write down new last playdate in 'DD.MM.YYYY' format and press Enter\n(Empty or invalid => skip)");
            string newPlaytime = Console.ReadLine();
            if (!newPlaytime.Equals(""))
            {
                try
                {
                    newLastPlayed = DateTime.ParseExact(newPlaytime, "dd.MM.yyyy", null);
                    if ((DateTime.Now - newLastPlayed).TotalDays < 0)
                    {
                        Console.WriteLine($"Playdate edit skipped, { newPlaytime } is invalid.\n");
                        goto dateSkip;
                    }
                    songCopy.LastPlaytime = newLastPlayed;
                    Console.WriteLine($"{ newLastPlayed:dd/MM/yyyy} set to last playdate!\n");
                }
                catch
                {
                    Console.WriteLine($"Playdate edit skipped, { newPlaytime } is invalid.\n");
                    newPlaytime = "";
                }
            }
            else
            {
                Console.WriteLine("Playdate edit skipped\n");
            }
            dateSkip:
            Console.WriteLine("Write down new priority from 1 - 10 and press Enter\n(Empty or invalid => skip)");
            string newPriority = Console.ReadLine();
            if (!newPriority.Equals(""))
            {
                try
                {
                    int number = int.Parse(newPriority.ToString());
                    if (number <= 0 || number > 10)
                    {
                        Console.WriteLine("Priority edit skipped\n");
                        newPriority = "";
                    }
                    else
                    {
                        Console.WriteLine($"{ newPriority } set to new priority!\n");
                        songCopy.Priority = number;
                    }
                }
                catch
                {
                    Console.WriteLine($"Skipped, { newPriority } is invalid.\n");
                    newPriority = "";
                }
            }
            else
            {
                Console.WriteLine("Priority edit skipped\n");
            }
            Console.WriteLine("Write down new latest performance from 1 - 10 and press Enter\n(Empty or invalid => skip)");
            string newPerformance = Console.ReadLine();
            if (!newPerformance.Equals(""))
            {
                try
                {
                    int number = int.Parse(newPerformance.ToString());
                    if (number <= 0 || number > 10)
                    {
                        Console.WriteLine("Latest performance edit skipped\n");
                        newPerformance = "";
                    }
                    else
                    {
                        Console.WriteLine($"{ newPerformance } set to new latest performance!\n");
                        songCopy.LastPlayedPerformance = number;
                    }
                }
                catch
                {
                    Console.WriteLine($"Skipped, { newPerformance } is invalid.\n");
                    newPerformance = "";
                }
            }
            else
            {
                Console.WriteLine("Latest performance edit skipped\n");
            }
            if (newNameEdited)
            {
                var deletedList = saveFile.DeleteSong(allSongs, selectedSong);
                var addedList = saveFile.AddSong(deletedList, songCopy);
                saveFile.SaveSongs(addedList);
            }
            else
            {
                var editedList = saveFile.EditSong(allSongs, songCopy);
                saveFile.SaveSongs(editedList);
            }
            Console.WriteLine($"Editing is done!\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine("");
        }
    }
}

/*
            //Yks hyvä esimerkki probleema tuli vastaan bugitestauksessa: 
            var song1 = new Song();
            song1.Name = "Jannen laulu";

            var song2 = new Song();
            song2.Name = song1.Name;

            var song3 = song1;
            song3.Name = "Sibelius laulu";

            //Mitä konsoli tulostaa?
            Console.WriteLine("Song1 name: " + song1.Name);
            Console.WriteLine("Song2 name: " + song2.Name);
            Console.WriteLine("Song3 name: " + song3.Name);
            */