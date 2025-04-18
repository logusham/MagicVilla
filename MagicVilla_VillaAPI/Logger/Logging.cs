﻿namespace MagicVilla_VillaAPI.Logger
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error - " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }else if(type == "warning")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Warning - " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
