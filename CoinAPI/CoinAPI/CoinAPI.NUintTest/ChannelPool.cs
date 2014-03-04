using KJFramework.Net.Channels;
using KJFramework.Net.Transaction.Agent;
using KJFramework.Net.Transaction.Messages;
using MobiSage.AdsAPI.BAS.ProtocolStack;

namespace MobiSage.AdsAPI.ABS.NUintTest
{
    internal static class ChannelPool
    {
        #region Members

        private static ConnectionAgent _agent; 

        #endregion

        #region Methods

        public static ConnectionAgent GetAgent()
        {
            lock (typeof(ChannelPool))
            {
                if (_agent != null && _agent.GetChannel().IsConnected) return _agent;
                TcpTransportChannel channel = new TcpTransportChannel("172.16.20.18", 20200);
                channel.Connect();
                if (!channel.IsConnected) return null;
                _agent = new ConnectionAgent(new MessageTransportChannel<BaseMessage>(channel, new BASProtocolStack()), Common.TransactionManager);
                return _agent;
            }
        }

        #endregion
    }
}