using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SongTracker.Classes
{
    class SongEditorList
    {
        static string inputString;
        static int inputInt;
        static ConsoleCommand conCom = new ConsoleCommand();
        public SongMemory saveFile = new SongMemory();
        private List<Song> allSongs = new List<Song>();
        public void DisplaySongs()
        {
            while (true)
            {
                //PRINT SONGS
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
                inputString = conCom.AskInputKey("A = Add new song\nS = Select song\nB = End editor");
                //END EDITOR
                if (inputString.Equals("B"))
                {
                    inputString = conCom.AskInputKey("Press Y to confirm End editor");
                    if (inputString.Equals("Y"))
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
                //ADD SONG
                if (inputString.Equals("A"))
                {
                    Console.WriteLine("Adding new song...");
                    CreateSong();
                }
                //SELECT SONG
                if (inputString.Equals("S"))
                {
                    inputInt = conCom.AskInputNumber("Write down a song number and press Enter");
                    if(inputInt >= 1 && inputInt <= 10)
                    {
                        var selectedSong = allSongs[inputInt - 1];
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{ selectedSong.Name }");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" selected");
                        inputString = conCom.AskInputKey("A = Edit song\nD = Delete song\nB = Cancel\n");
                        //EDIT SONG
                        if (inputString.Equals("A"))
                        {
                            EditSong(selectedSong);
                        }
                        //DELETE SONG
                        if (inputString.Equals("D"))
                        {
                            inputString = conCom.AskInputKey("Press Y to confirm deletion");
                            if (inputString.Equals("Y"))
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
                    else
                    {
                        Console.WriteLine($"Not a valid number!\n");
                    }
                }
            }
        }
        public void CreateSong()
        {
            var newSong = new Song();
            //ADD NAME
            inputString = conCom.AskInputString("Write down a new song name and press Enter\n(Empty => cancel)");
            if (!inputString.Equals(""))
            {
                newSong.Name = inputString;
                if (saveFile.songExists(saveFile.GetSongs(), newSong))
                {
                    Console.WriteLine($"Add new song cancelled, { newSong.Name } already exists!\n");
                    conCom.AskInputKey("Press any key to continue...");
                    return;
                }
                else
                {
                    Console.WriteLine($"{ inputString } set to new name!\n");
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            //ADD DATEIME
            DateTime newLastPlayed;
            inputString = conCom.AskInputString("Write down last playdate in 'DD.MM.YYYY' format and press Enter\n(Empty or invalid => cancel)");
            if (!inputString.Equals(""))
            {
                newLastPlayed = conCom.StringToDatetime(inputString);
                if(conCom.IsValidDatetime(newLastPlayed))
                {
                    newSong.LastPlaytime = newLastPlayed;
                    Console.WriteLine($"{ newLastPlayed:dd/MM/yyyy} set to last playdate!\n");
                }
                else
                {
                    Console.WriteLine($"Add last playdate cancelled, { inputString } is invalid.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            //ADD PRIORITY
            inputInt = conCom.AskInputNumber("Write down a song priority from 1 - 10 and press Enter\n(Empty or invalid => cancel)");
            if (!inputInt.Equals(""))
            {
                if (inputInt >= 0 && inputInt <= 10)
                {
                    newSong.Priority = inputInt;
                    Console.WriteLine($"{ inputInt } set to new priority!\n");
                }
                else 
                {
                    Console.WriteLine($"Add new song cancelled, invalid number.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            //ADD PERFORMANCE
            inputInt = conCom.AskInputNumber("Write down your latest song performance from 1 - 10 and press Enter\n(Empty or invalid => cancel)");
            if (!inputInt.Equals(""))
            {
                if (inputInt >= 0 && inputInt <= 10)
                {
                    newSong.LastPlayedPerformance = inputInt;
                    Console.WriteLine($"{ inputInt } set to new latest performance!\n");
                }
                else
                {
                    Console.WriteLine($"Add new song cancelled, invalid number.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Add new song cancelled.\n");
                return;
            }
            //SAVE ADDED SONG
            Console.WriteLine("Add new song completed.\n");
            var savedList = saveFile.AddSong(saveFile.GetSongs(), newSong);
            saveFile.SaveSongs(savedList);
            conCom.AskInputKey("Press any key to continue...");
        }
        public void EditSong(Song selectedSong)
        {
            var booleanSong = new Song();
            var songCopy = selectedSong;
            Console.WriteLine($"Editing { songCopy.Name }");
            //EDIT NAME
            inputString = conCom.AskInputString("Write down new song name and press Enter\n(Empty => skip)");
            if (!inputString.Equals(""))
            {
                booleanSong.Name = inputString;
                if (saveFile.songExists(saveFile.GetSongs(), booleanSong) && (selectedSong.Name != booleanSong.Name))
                {
                    Console.WriteLine($"Song name { booleanSong.Name } already exists! Cancelling...");
                    conCom.AskInputKey("Press any key to continue...");
                    return;
                }
                songCopy.Name = inputString;
                Console.WriteLine($"{ inputString } set to new name!\n");
            }
            else
            {
                Console.WriteLine("Name edit skipped\n");
            }
            //EDIT DATETIME
            DateTime newLastPlayed;
            inputString = conCom.AskInputString("Write down new last playdate in 'DD.MM.YYYY' format and press Enter\n(Empty or invalid => skip)");
            if (!inputString.Equals(""))
            {
                newLastPlayed = conCom.StringToDatetime(inputString);
                if(conCom.IsValidDatetime(newLastPlayed))
                {
                    songCopy.LastPlaytime = newLastPlayed;
                    Console.WriteLine($"{ newLastPlayed:dd/MM/yyyy} set to last playdate!\n");
                }
                else
                {
                    Console.WriteLine($"Playdate edit skipped, { inputString } is invalid.\n");
                }
            }
            else
            {
                Console.WriteLine("Playdate edit skipped\n");
            }
            //EDIT PRIORITY
            inputInt = conCom.AskInputNumber("Write down new priority from 1 - 10 and press Enter\n(Empty or invalid => skip)");
            if (!inputInt.Equals(""))
            {
                if(inputInt >= 0 && inputInt <= 10)
                {
                    Console.WriteLine($"{ inputInt } set to new priority!\n");
                    songCopy.Priority = inputInt;
                }
                else
                {
                    Console.WriteLine("Priority edit skipped\n");
                }
            }
            else
            {
                Console.WriteLine("Priority edit skipped\n");
            }
            //EDIT PERFORMANCE
            inputInt = conCom.AskInputNumber("Write down new latest performance from 1 - 10 and press Enter\n(Empty or invalid => skip)");
            if (!inputInt.Equals(""))
            {
                if (inputInt >= 0 && inputInt <= 10)
                {
                    Console.WriteLine($"{ inputInt } set to new latest performance!\n");
                    songCopy.LastPlayedPerformance = inputInt;
                }
                else
                {
                    Console.WriteLine("Latest performance edit skipped\n");
                }
            }
            else
            {
                Console.WriteLine("Latest performance edit skipped\n");
            }
            //SAVE EDITED SONG
            var editedList = saveFile.EditSong(allSongs, songCopy);
            saveFile.SaveSongs(editedList);
            Console.WriteLine($"Editing is done!\n");
            conCom.AskInputKey("Press any key to continue...");
        }
    }
}