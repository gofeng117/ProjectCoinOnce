using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinAPI.CommonFramework.Exceptions
{
    /// <summary>
    ///     指定关系未找到异常
    /// </summary>
    public class RelationNotFoundException : System.Exception
    {
        #region Constructor

        /// <summary>
        ///     指定关系未找到异常
        /// </summary>
        public RelationNotFoundException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
