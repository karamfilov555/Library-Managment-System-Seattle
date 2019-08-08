﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IGlobalMessages
    {
        string PleaseLoginOrRegisterMessage();
        string WrongCredentialsMessage();
        string SuccessfullyLoginMessage();
        string LogOutMessage();
        string RegisterMessage(string username);
        string ThisUserAlreadyExistMessage();
        string InvalidParametersMessage();
        string BookCreated();
        string CancleMemership_PasswordRequiredMessage();
        string CancleMemershipMessage();
        string WrongPasswordMessage();
    }
}