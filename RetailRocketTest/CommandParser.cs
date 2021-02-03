using RetailRocketTest.Commands;
using RetailRocketTest.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RetailRocketTest
{
    class CommandParser
    {
        private readonly IMessageReceiver consoleManager;
        private readonly ShopRepository shopRepository;
        private readonly YmlParser ymlParser;

        public CommandParser(IMessageReceiver consoleManager)
        {
            this.consoleManager = consoleManager;

            shopRepository = new ShopRepository(new AppDBContent());
            ymlParser = new YmlParser();
        }


        public ICommand ParseCommand(string strCommand)
        {
            bool isSuccess = true;
            ICommand command1 = null;
            if (!String.IsNullOrWhiteSpace(strCommand))
            {
                try
                {
                    Regex regex = new Regex("dotnet run -- (?<command>[a-zA-Z]+?) (?<params>.*)");
                    Match match = regex.Match(strCommand);

                    if (match.Success)
                    {
                        if (match.Groups["command"].Value.Equals("save"))
                        {
                            command1 = ParseSaveCommandParams(match.Groups["params"].Value);
                        }
                        else if (match.Groups["command"].Value.Equals("print"))
                        {
                            command1 = ParsePrintCommandParams(match.Groups["params"].Value);
                        }
                    }
                }
                catch (Exception e)
                {
                    SendErrorMessage($"Команда \"{strCommand}\":{ Environment.NewLine}{e.Message}");
                }
            }


            if (command1 == null)
            {
                SendErrorMessage($"Не удалось обработать команду \"{strCommand}\"");
            }

            return command1;
        }
        private ICommand ParseSaveCommandParams(string strParams)
        {
            ICommand command = null;

            Regex regex = new Regex("<(?<shopId>.+?)>( <(?<url>.+?)>)?");
            Match match = regex.Match(strParams);

            string shopId = match.Groups["shopId"].Value;
            string url = match.Groups["url"].Value;
            if (String.IsNullOrWhiteSpace(shopId) || String.IsNullOrWhiteSpace(url))
            {
            }
            else
            {
                command = new SaveCommand(consoleManager, shopRepository, ymlParser, shopId, url);
            }

            return command;
        }

        private ICommand ParsePrintCommandParams(string strParams)
        {
            ICommand command = null;

            Regex regex = new Regex("<(?<shopId>.+?)>");
            Match match = regex.Match(strParams);

            string shopId = match.Groups["shopId"].Value;

            if (String.IsNullOrWhiteSpace(shopId))
            {
            }
            else
            {
                command = new PrintCommand(consoleManager, shopRepository, shopId);
            }

            return command;
        }

        private void SendErrorMessage(string message)
        {
            consoleManager.WriteCommandResultMessage(message);
        }

    }
}
