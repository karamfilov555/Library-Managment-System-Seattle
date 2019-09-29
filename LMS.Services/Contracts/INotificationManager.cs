using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface INotificationManager
    {
        string RenewBookDescription(string username, DateTime newDateTime, string title);
    }
}
