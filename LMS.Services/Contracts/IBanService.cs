using LMS.DTOs;
using LMS.Models;
using LMS.Models.Models;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IBanService
    {
        Task<BanRecord> AddBan(BanDto banDto);
        Task<BanRecord> CheckIfUserIsBanned(string inputUsername);
    }
}
