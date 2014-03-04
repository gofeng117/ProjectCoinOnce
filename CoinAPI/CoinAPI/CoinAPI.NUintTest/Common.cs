using KJFramework.Net.Transaction.Managers;
using MobiSage.AdsAPI.CommonFramework.Core;
using MobiSage.AdsAPI.CommonFramework.Enums;

namespace MobiSage.AdsAPI.ABS.NUintTest
{
    internal static class Common
    {
        public static MessageTransactionManager TransactionManager = new MessageTransactionManager(30000);
        private static bool _initialized = false;

        public static void Initialize()
        {
            lock (typeof(Common))
            {
                if(!_initialized)
                {
                    SystemWorker.Instance.Initialize(ServiceRoles.UAS);
                    _initialized = true;
                }
            }
        }
    }
}