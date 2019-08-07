using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.IO
{
    public class InputReader : IInputReader
    {
        public InputReader()
        {

        }
        public string ReadLine()
        {
            var laneToRead = Console.ReadLine();
            return laneToRead;
        }

    }
}
