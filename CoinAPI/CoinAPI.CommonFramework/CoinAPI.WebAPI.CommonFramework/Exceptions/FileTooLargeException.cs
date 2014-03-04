using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinAPI.CommonFramework.Exceptions
{
    /// <summary>
    ///     上传文件太大异常
    /// </summary>
    public class FileTooLargeException : System.Exception
    {
        #region Constructor

        /// <summary>
        ///     上传文件太大异常
        /// </summary>
        public FileTooLargeException(string message)
            : base(message)
        {

        }

        #endregion
    }
}
