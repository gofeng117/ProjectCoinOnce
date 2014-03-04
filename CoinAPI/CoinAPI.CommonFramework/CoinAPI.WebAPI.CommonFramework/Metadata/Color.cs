using System;
using System.Text;

namespace CoinAPI.CommonFramework.Metadata
{
    /// <summary>
    ///     颜色对象
    /// </summary>
    public sealed class Color
    {
        #region Constructor

        /// <summary>
        ///     颜色对象
        /// </summary>
        /// <param name="color">
        ///     颜色信息
        ///     <para>* 格式必须为: #XXXXXX</para>
        /// </param>
        public Color(string color)
        {
            if (string.IsNullOrEmpty(color)) throw new ArgumentNullException("color");
            if (color.Length != 7) throw new ArgumentException("Illegal Color class metadata, the target byte array size *MUST* be 7.");
            _value = color.Substring(1, 6);
        }

        /// <summary>
        ///     颜色对象
        /// </summary>
        /// <param name="data">
        ///     颜色信息数据
        ///     <para>* 数据仅应该包括颜色数据, 并且长度必须为: 6</para>
        /// </param>
        public Color(byte[] data)
            : this(data, 0, data.Length)
        {
        }

        /// <summary>
        ///     颜色对象
        /// </summary>
        /// <param name="data">
        ///     颜色信息数据
        ///     <para>* 数据仅应该包括颜色数据, 并且长度必须为: 6</para>
        /// </param>
        /// <param name="offset">偏移量</param>
        /// <param name="len">长度</param>
        public Color(byte[] data, int offset, int len)
        {
            if (data == null || data.Length < len || len != 6) throw new ArgumentException("Illegal Color class metadata, the target byte array size *MUST* be 6.");
            _value = Encoding.ASCII.GetString(data, offset, len);
        }

        #endregion

        #region Members

        private readonly string _value;
        /// <summary>
        ///     获取内部颜色信息
        /// </summary>
        public byte[] Data
        {
            get { return Encoding.ASCII.GetBytes(_value); }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Format("#{0}", _value);
        }

        #endregion
    }
}