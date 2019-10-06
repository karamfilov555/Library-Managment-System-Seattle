using LMS.Data;
using LMS.DTOs;
using LMS.Models.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class BanService : IBanService
    {
        private readonly LMSContext _context;
        public BanService(LMSContext context)
        {
            _context = context;
        }
        public async Task<BanRecord> CheckIfUserIsBanned(string inputUsername)
        {
            var userBanned = await _context.Users.FirstOrDefaultAsync(u => u.UserName == inputUsername && u.BanRecordId != null).ConfigureAwait(false);
            if (userBanned != null)
            {
                var banRecordsOfUser = _context.BanRecords.Where(br => br.UserId == userBanned.Id);
                var longestDate = banRecordsOfUser.Max(b => b.ExpirationDate);
                var ban = banRecordsOfUser.FirstOrDefault(b => b.ExpirationDate == longestDate);
                return ban;
            }

            return null;
        }


        public async Task<BanRecord> FindBanRecordByID(string userId)
            => await _context.BanRecords.FirstOrDefaultAsync(i => i.UserId == userId).ConfigureAwait(false);

        public async Task<BanRecord> AddBan(BanDto banDto)
        {
            var ban = new BanRecord
            {
                UserId = banDto?.UserId,
                Description = banDto.Description,
                ExpirationDate = banDto.ExpirationDate,
            };
            await _context.BanRecords.AddAsync(ban).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ban.UserId).ConfigureAwait(false);
            user.BanRecordId = ban.Id;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return ban;
        }
    }
}
