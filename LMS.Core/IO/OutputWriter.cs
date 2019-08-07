using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.IO
{
    public class OutputWriter : IOutputWriter
    {
        public OutputWriter()
        {
            
        }
        public void WriteLine(string lane)
        {
            Console.WriteLine(lane);
        }
    }
}
