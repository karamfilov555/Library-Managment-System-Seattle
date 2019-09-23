using LMS.DTOs;
using LMS.Web.Models;
using System.Threading.Tasks;

namespace LMS.Web.Mappers.Contracts
{
    public interface IMapVmToDTO
    {
        Task<BookDTO> MapBookVmToDTO(BookViewModel bookVm);
        Task<BanDto> MapBanVmToDto(BanViewModel banVm);
    }
}
