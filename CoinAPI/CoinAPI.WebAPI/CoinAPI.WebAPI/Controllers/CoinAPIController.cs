using System;
using CoinAPI.Domain.Business;
using CoinAPI.Domain.Midiator.Proxies;
using CoinAPI.Domain.Enums;
using System.Threading.Tasks;
using System.Web.Http;
using CoinAPI.WebAPI.Handlers;
using IExecuteResult = CoinAPI.Domain.Results.IExecuteResult;

namespace CoinAPI.WebAPI.Controllers
{
    /// <summary>
    ///     CoinAPI控制器
    /// </summary>
    public class CoinAPIController : ApiController
    {
        #region Methods

        /// <summary>
        ///     异步获取指定账户编号下指定Ticker编号的Ticker
        /// </summary>
        /// <param name="cid">帐户编号</param>
        /// <param name="pid">广告活动编号</param>
        /// <returns>执行成功返回CoinTicker的网络实体对象，执行失败返回错误的实体结构ErrorModel</returns>
        [HttpGet]
        public async Task<object> GetTickerAsync(byte cid, byte pid)
        {
            try
            {
                IExecuteResult executeResult =
                    await CoinAPIRemoteProxy.Instance.GetTickerAsync((CoinTypes) cid, (PlatformTypes) pid);
                if (executeResult.State != ExecuteResults.Succeed)
                    return new ErrorModel {ErrorId = executeResult.ErrorId, Desc = executeResult.Error};
                //return new ErrorModel { ErrorId = executeResult.ErrorId, Desc = ErrorManager.GetErrorMessageById(executeResult.ErrorId) };
                return executeResult.GetResult<Ticker>();
            }
            catch (Exception ex)
            {
                return ExceptionManager.HandleException(ex);
            }
        }

        #endregion
    }
}