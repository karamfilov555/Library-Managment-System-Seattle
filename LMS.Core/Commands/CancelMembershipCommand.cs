using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CancelMembershipCommand : ICommand
    {
        public CancelMembershipCommand()
        {

        }
        public string Execute(IList<string> parameteres)
        {
            return "cancelMember";
        }
    }
}
