using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.IO
{
    public class InputKeyReader : IInputKeyReader
    {
        public InputKeyReader()
        {

        }
        public string ReadKeys()
        {
            string secret = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                secret += key.KeyChar;
            }
            return secret;
        }
    }
}
