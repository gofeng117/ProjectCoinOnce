using System;
using KJFramework.Encrypt;
using KJFramework.Tracing;
using CoinAPI.Domain.Enums;

namespace CoinAPI.Domain.Service
{
    public class KeyParse
    {
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(KeyParse));

        public CoinTypes CoinType { get; set; }
        public PlatformTypes PlatformType { get; set; }

        public unsafe override string ToString()
        {
            byte[] data = new byte[2];
            fixed (byte* pByte = data)
            {
                *(pByte) = (byte)CoinType;
                *(pByte + 1) = (byte)PlatformType;
            }
            return Convert.ToBase64String(EncryptTEAHelper.Encrypt(data));
        }

        public unsafe static bool TryParse(string value, out KeyParse key)
        {
            if (string.IsNullOrEmpty(value))
            {
                key = null;
                return false;
            }
            try
            {
                byte[] preData = Convert.FromBase64String(value);
                byte[] deData = EncryptTEAHelper.Decrypt(preData);
                fixed (byte* pByte = deData)
                {
                    key = new KeyParse {CoinType = (CoinTypes) (*(pByte)), PlatformType = (PlatformTypes) (*(pByte + 1))};
                    return true;
                }
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                key = null;
                return false;
            }
        }
    }
}
