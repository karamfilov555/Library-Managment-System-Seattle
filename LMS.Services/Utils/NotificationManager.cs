using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services.Utils
{
    public class NotificationManager : INotificationManager
    {
        private const string renewBookMsg = "User: {0}, renew return date to {1} of a book \"{2}\"!";

        public string RenewBookDescription(string username, DateTime newDateTime, string title)
        => string.Format(renewBookMsg, username, newDateTime, title);

    }
}
