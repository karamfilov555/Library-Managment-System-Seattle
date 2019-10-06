using LMS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Tests
{
    public static class TestUtilities
    {
        public static DbContextOptions<LMSContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<LMSContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
