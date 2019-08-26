using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Utils
{
    public class LogoPrinter : ILogoPrinter
    {
        public LogoPrinter()
        {

        }
        public void PrintLogo()
        {
            int n = 14;
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("{0}{1}{2}", new string(' ', 18), new string('/', n / n), new string('^', n / 2), new string('\\', n / n));
            Console.Write("{0}", new string('_', n - 3));
            Console.WriteLine("{0}{1}{2}", new string('/', n / n), new string('^', n / 2), new string('\\', n / n));

            for (int i = 0; i < n - 12; i++)
            {
                Console.Write("{0}{1}{2}{3}", new string(' ', 18), new string('|', 1), new string(' ', (2 * n) - 2), new string('|', 1));
                Console.WriteLine();
            }
            Console.Write("{0}", new string(' ', 18));
            Console.WriteLine("|Central Library in Seattle|");
            for (int i = 0; i < 2; i++)
            {
                Console.Write("{0}{1}{2}{3}", new string(' ', 18), new string('|', 1), new string(' ', (2 * n) - 2), new string('|', 1));
                Console.WriteLine();
            }
            Console.WriteLine("{0}{1}{2}{3}{4}{5}", new string(' ', 18), new string('|', 1), new string(' ', 8), new string('_', 10), new string(' ', 8), new string('|', 1));
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}", new string(' ', 18), new string('|', 1), new string(' ', 8), new string('|', 1),
                new string(' ', 8), new string('|', 1), new string(' ', 8), new string('|', 1));
            }
            Console.WriteLine("{0}{1}{2}{3}{4}|", new string(' ', 18), new string('|', 1), new string(' ', n / 2 + 1), new string('_', n - 4), new string(' ', n / 2 + 1));

            Console.Write("{0}{1}{2}{3}", new string(' ', 18), new string('\\', 1), new string('_', n / 2), new string('/', 1));

            Console.Write("{0}", new string(' ', n - 4));
            Console.WriteLine("{0}{1}{2}", new string('\\', 1), new string('_', n / 2), new string('/', 1));
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
