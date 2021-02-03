using System;
using System.Collections.Generic;
using System.Text;

namespace RetailRocketTest
{
    class ConsoleManager : IMessageReceiver
    {
        private CommandExecutor commandExecutor;

        public ConsoleManager()
        {
            commandExecutor = new CommandExecutor(this);
        }

        public void Start()
        {
            Console.WriteLine("Введите команду");
            while (true)
            {
                string str = Console.ReadLine();

                commandExecutor.AddCommand(str);
            }
        }

        public void WriteCommandResultMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
