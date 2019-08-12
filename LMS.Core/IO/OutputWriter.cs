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
        public void WriteLine()
        {
            Console.WriteLine();
        }
        public void Write(string text)
        {
            Console.Write(text);
        }
        public void Write(string t, string e, string x, string c, string s)
        {
            Console.Write(t+e+x+c+s);
        }
    }
}
