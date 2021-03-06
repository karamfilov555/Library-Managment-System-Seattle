﻿using LMS.Services.Contracts;
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
        private const string availableBookMsg = "{0}, book \"{1}\" that you reserved is now available!!!";
        private const string bookWasGivenMsg = "Transfer book: {0}, the book \"{1}\" that you reserved is already given to you!!!";
        private const string transferBookMsg = "Transfer book notification: User \"{0}\" has returned a book \"{1}\", and user with username: \"{2}\" recive it, because he/she was first on reservation queque!";
        private const string quickMassageFormMsg = "From Quick message form: \"{0}\" --> Email to reply: {1}";
        private const string cancelMembershipMsg = "Cancel Membership notification: User with username \"{0}\" and Id: {1} just cancel his membership!";


        public string RenewBookDescription(string username, DateTime newDateTime, string title)
        => string.Format(renewBookMsg, username, newDateTime, title);
        public string ReturnBookDescription(string username, string title)
        => string.Format(returnBookMsg, username, title);
        public string CheckOutBookDescription(string username, string title)
        => string.Format(checkOutBookMsg, username, title);
        public string ReserveBookDescription(string username, string title)
       => string.Format(reserveBookMsg, username, title);
        public string AvailableBookDescription(string username, string title)
            => string.Format(availableBookMsg, username, title);
        public string BookWasGivenToUser(string username, string title)
           => string.Format(bookWasGivenMsg, username, title);
        public string TransferBookDescription(string userWhoReturnBook,string userWhoReciveBook, string title)
         => string.Format(transferBookMsg, userWhoReturnBook,title, userWhoReciveBook);
        public string QuickMessageDescription(string message, string email)
          => string.Format(quickMassageFormMsg, message, email);
        public string CancelMembershipDescription(string username, string userId)
         => string.Format(cancelMembershipMsg, username, userId);
    }
}
