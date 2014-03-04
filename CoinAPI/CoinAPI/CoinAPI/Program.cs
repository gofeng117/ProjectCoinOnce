using System;
using KJFramework.Dynamic.Components;
using KJFramework.Dynamic.Structs;

namespace CoinAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicDomainService service = new DynamicDomainService(new ServiceDescriptionInfo
            {
                Description = "CoinAPI业务服务",
                Name = "Coin API Business Service",
                ServiceName = "CoinAPI",
                Version = "0.0.0.1"
            });
            service.Start();
            Console.ReadLine();
        }
    }
}
