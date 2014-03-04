using System.Collections.Generic;
using System.Web.Mvc;
using CoinAPI.WebAPI.DataHelper;
using CoinAPI.WebAPI.DataRepository;
using CoinAPI.WebAPI.Handlers;

namespace CoinAPI.WebAPI.Controllers
{
    /// <summary>
    ///     网站首页控制器
    /// </summary>
    public class HomeController : Controller
    {
        #region Members

        private DataBaseHelper _helper = new DataBaseHelper();

        #endregion

        #region Methods

        /// <summary>
        ///     获取Api的首页展示信息
        /// </summary>
        /// <returns>返回获取到的信息</returns>
        public ActionResult Index()
        {
            IList<WebApi_Group> list = null;
            try
            {
                list = _helper.GetApiCroupList();
            }
            catch (System.Exception ex)
            {
                ErrorLog.WriteError(ex);
            }
            return View(list);
        }

        #endregion
    }
}
