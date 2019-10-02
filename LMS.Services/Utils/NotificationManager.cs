using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services.Utils
{
    public class NotificationManager : INotificationManager
    {
        private const string renewBookMsg = "Renew book notification: User: {0}, renew return date to {1} of a book \"{2}\"!";
        private const string returnBookMsg = "Return book notification: User: {0}, just return a book \"{1}\"!";
        private const string checkOutBookMsg = "Check-Out book notification: User: {0}, just check-out a book \"{1}\"!";
        private const string reserveBookMsg = "Reserve book notification: User: {0}, just reserve a book \"{1}\"!";

        public string RenewBookDescription(string username, DateTime newDateTime, string title)
        => string.Format(renewBookMsg, username, newDateTime, title);

        public string ReturnBookDescription(string username, string title)
        => string.Format(returnBookMsg, username, title);

        public string CheckOutBookDescription(string username, string title)
        => string.Format(checkOutBookMsg, username, title);
        public string ReserveBookDescription(string username, string title)
       => string.Format(reserveBookMsg, username, title);
    }
}
