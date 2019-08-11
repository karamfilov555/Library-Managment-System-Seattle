using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IGlobalMessages
    {
        string PleaseLoginOrRegisterMessage();
        string WrongCredentialsMessage();
        string SuccessfullyLoginMessage(string username);
        string LogOutMessage();
        string RegisterMessage(string username);
        string ThisUserAlreadyExistMessage();
        string InvalidParametersMessage();
        string BookCreated();
        string CancelMemership_PasswordRequiredMessage();
        string CancelMemershipMessage();
        string WrongPasswordMessage();
        string BookRemovedMessage(string title);
        string PrintAddBookLabel();
        string CatalogDelimiter(int counter);
        string BookCheckedOutMessage(string title);
        string PrintCheckOutBookLabel();
        string WichBookYouWantToReturnMessage();
        string SuccessfullyReturnBookMessage(string title);
    }
}
