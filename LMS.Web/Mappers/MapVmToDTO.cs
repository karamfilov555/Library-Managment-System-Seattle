using LMS.DTOs;
using LMS.Web.Mappers.Contracts;
using LMS.Web.Models;
using System.Threading.Tasks;

namespace LMS.Web.Mappers
{
    public class MapVmToDTO : IMapVmToDTO
    {
        // Тук можем да филтрираме още един път информацията от вю-то , преди да я подадем на бизнес лейъра
        // (ViewModel и DataTransferObject НЕ Е едно и също !) , btw пази ни и от депенденси проблеми
        public async Task<BookDTO> MapBookVmToDTO(BookViewModel bookVm)
            => new BookDTO
            {
                Title = bookVm.Title,
                AuthorName = bookVm.AuthorName,
                Year = bookVm.Year,
                Pages = bookVm.Pages,
                SubjectCategoryName = bookVm.SubjectCategoryName,
                Copies = bookVm.Copies,
                Language = bookVm.Language,
                Country = bookVm.Country,
                CoverImageUrl = bookVm.CoverImageUrl,
            };
        public async Task<BanDto> MapBanVmToDto(BanViewModel banVm)
            => new BanDto
            {
                UserId = banVm.UserId,
                Username = banVm.Username,
                Description = banVm.Description,
                ExpirationDate = banVm.ExpirationDate,
            };
    }
}
