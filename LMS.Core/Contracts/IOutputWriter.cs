using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Contracts
{
    public interface IOutputWriter
    {
        void WriteLine(string lane);
        void WriteLine();
        void Write(string text);
        void Write(string t, string e, string x, string c, string s);
    }
}
