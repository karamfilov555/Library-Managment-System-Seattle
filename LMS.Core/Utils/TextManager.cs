using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Core.Utils
{
    public class TextManager : ITextManager
    {
        public TextManager()
        {

        }
        public string ExtractCommandName(string input)
        {
            var commandName = input.Split()[0];
            return commandName;
        }
        public IEnumerable<string> GetCommandParams(string input)
        {
            var laneParameters = input.Trim().Split(
                        new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var parameters = laneParameters.Skip(1);
            return parameters;
        }
        public string GetParams(IList<string> parameteres)
        {
            var strBuilder = new StringBuilder();
            foreach (var item in parameteres)
            {
                strBuilder.Append(item + " ");
            }
            return strBuilder.ToString().Trim();
        }
    }
}
