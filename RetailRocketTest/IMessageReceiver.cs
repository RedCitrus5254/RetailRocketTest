using System;
using System.Collections.Generic;
using System.Text;

namespace RetailRocketTest
{
    interface IMessageReceiver
    {
        void WriteCommandResultMessage(string message);
    }
}
