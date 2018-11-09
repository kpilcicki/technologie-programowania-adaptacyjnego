using BusinessLogic.API;
using System;

namespace CommandLineGUI
{
    class ConsoleUserInfo : IUserInfo
    {
        public void PromptUser(string message, string caption)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(caption + " ");
            Console.ResetColor();
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
