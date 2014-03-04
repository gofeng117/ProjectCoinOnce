using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository
{
    public interface IAdCreativeRepository
    {
        IExecuteResult CreateTextAd(uint userId, ulong accountId, TextAdCreative adCreative);
    }
}
