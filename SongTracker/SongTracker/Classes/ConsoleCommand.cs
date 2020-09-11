using System;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class ConsoleCommand
    {
        public string AskInputString(string text)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();
            Console.WriteLine("");
            return input;
        }
        public string AskInputKey(string text)
        {
            Console.WriteLine(text);
            char input = Console.ReadKey().KeyChar;
            input = char.ToUpper(input);
            Console.WriteLine("");
            return input.ToString();
        }
        public int AskInputNumber(string text)
        {
            Console.WriteLine(text);
            string newNumber = Console.ReadLine();
            Console.WriteLine("");
            if (!newNumber.Equals(""))
            {
                try
                {
                    int number = int.Parse(newNumber.ToString());
                    return number;
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        public bool IsValidDatetime(DateTime datetime)
        {
            if((DateTime.Now - datetime).TotalDays < 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
        public DateTime StringToDatetime(string stringDatetime)
        {
            DateTime newDatetime;
            try
            {
                newDatetime = DateTime.ParseExact(stringDatetime, "dd.MM.yyyy", null);
                return newDatetime;
            }
            catch
            {
                newDatetime = DateTime.ParseExact("11.11.9999", "dd.MM.yyyy", null);
                return newDatetime;
            }
        }
    }
}
