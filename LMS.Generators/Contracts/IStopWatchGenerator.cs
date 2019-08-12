using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Generators.Contracts
{
    public interface IStopWatchGenerator  
    {
        void StartTime();
        void StopTime();
        double GetTime();
    }
}
