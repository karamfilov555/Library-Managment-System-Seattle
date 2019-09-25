using LMS.Data;
using LMS.DTOs;
using LMS.Models.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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
            var userBanned = await _context.Users.FirstOrDefaultAsync(u => u.UserName == inputUsername && u.BanRecordId != null);
            if (userBanned != null)
                return await FindBanRecordByID(userBanned.Id);

            return null;
        }


        public async Task<BanRecord> FindBanRecordByID(string userId)
            => await _context.BanRecords.FirstOrDefaultAsync(i => i.UserId == userId);

        public async Task<BanRecord> AddBan(BanDto banDto)
        {
            var ban = new BanRecord
            {
                UserId = banDto.UserId,
                Description = banDto.Description,
                ExpirationDate = banDto.ExpirationDate,
            };
            await _context.BanRecords.AddAsync(ban);
            await _context.SaveChangesAsync();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == ban.UserId);
            user.BanRecordId = ban.Id;
            await _context.SaveChangesAsync();
            return ban;
        }
    }
}
