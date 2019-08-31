using LMS.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Tests
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
