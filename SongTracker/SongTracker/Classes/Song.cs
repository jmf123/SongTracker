using System;
using System.Collections.Generic;
using System.Text;

namespace SongTracker.Classes
{
    class Song
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime LastPlaytime { get; set; }
        public int LastPlayedPerformance { get; set; }
        public Song()
        {
            LastPlaytime = DateTime.Now;
        }
        public int GetPriority()
        {
            double dateDifference = (DateTime.Now - LastPlaytime).TotalDays;
            if(dateDifference < 0)
            {
                dateDifference = 0;
            }
            else if (dateDifference >= 28)
            {
                dateDifference = 28;
            }
            int dateDifferenceFormatted = (int)dateDifference;
            var basePriority = (Priority + dateDifferenceFormatted + (10 - LastPlayedPerformance));
            return (basePriority * 2) + 6;
        }
        public void editorDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{ Name }");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($": Initial priority: { Priority }, Last playdate: { LastPlaytime:dd/MM/yyyy}, Last performance: { LastPlayedPerformance }");
        }
        public void sessionDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{ Name }");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($", Priority level: { GetPriority() }");
        }
    }
}
