using LMS.Data;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public class SqlCommandExecutor : ISqlCommandExecutor
    {
        private readonly LMSContext _context;
        public SqlCommandExecutor(LMSContext context)
        {
            _context = context;
        }

        public void ExecuteCommand(StringBuilder sqlCommand)
        {
            if (sqlCommand.Length != 0)
                _context.Database.ExecuteSqlCommand(sqlCommand.ToString());
        }
    }
}
