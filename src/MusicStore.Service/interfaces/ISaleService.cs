
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Service.interfaces
{
    public interface ISaleService
    {
        Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request);
        Task<BaseResponseGeneric<SaleRequestDto>> GetAsync(int id);
    }
}
