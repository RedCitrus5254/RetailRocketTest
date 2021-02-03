using RetailRocketTest.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RetailRocketTest
{
    class CommandExecutor
    {

        private ConcurrentQueue<ICommand> commands;
        private CommandParser commandParser;

        private Thread thread;

        public CommandExecutor(IMessageReceiver consoleManager)
        {
            commands = new ConcurrentQueue<ICommand>();
            commandParser = new CommandParser(consoleManager);

            thread = new Thread(new ThreadStart(StartThread))
            {
                IsBackground = true
            };
            thread.Start();

        }

        public void AddCommand(string command)
        {
            ICommand command1 = commandParser.ParseCommand(command);

            if (command1 != null)
            {
                commands.Enqueue(command1);
            }
        }

        private void StartThread()
        {
            while (true)
            {
                if (commands.TryDequeue(out ICommand command))
                {
                    command.Execute();
                }
            }
        }
    }
}
