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
        string ReserveBookDescription(string username, string title);
        string AvailableBookDescription(string username, string title);
        string BookWasGivenToUser(string username, string title);
        string TransferBookDescription(string userWhoReturnBook, string userWhoReciveBook, string title);
        string QuickMessageDescription(string message, string name);
    }
}
