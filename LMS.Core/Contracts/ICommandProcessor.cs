﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Contracts
{
    public interface ICommandProcessor
    {
        string ProcessCommand(string consoleInput);
    }
}