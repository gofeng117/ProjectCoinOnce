using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinAPI.CommonFramework.Exceptions
{
    /// <summary>
    ///     指定的同等尺寸未找到异常
    /// </summary>
    public class MatchSizeNotFoundException : System.Exception
    {
        #region Constructor

        /// <summary>
        ///     指定的同等尺寸未找到异常
        /// </summary>
        public MatchSizeNotFoundException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
