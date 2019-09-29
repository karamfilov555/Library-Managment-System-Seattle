using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface INotificationManager
    {
        string RenewBookDescription(string username, DateTime newDateTime, string title);
        string ReturnBookDescription(string username, string title);
        string CheckOutBookDescription(string username, string title);
    }
}
