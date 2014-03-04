using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Agent;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.Messages;
using KJFramework.Net.Transaction.Objects;
using KJFramework.Tracing;
using CoinAPI.CommonFramework.Processors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CoinAPI.CommonFramework.Schedulers
{
    /// <summary>
    ///     ����ַ������ṩ����صĻ�������
    /// </summary>
    public class RequestScheduler : IRequestScheduler
    {
        #region Members

        private object _lockObj = new object();
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof (RequestScheduler));
        private readonly IList<IConnectionAgent> _agents = new List<IConnectionAgent>();
        private readonly ConcurrentDictionary<Protocols, IMessageProcessor> _processors = new ConcurrentDictionary<Protocols, IMessageProcessor>();

        #endregion

        #region Implementation of IRequestScheduler

        /// <summary>
        ///     ע��һ�����Ӵ�����
        /// </summary>
        /// <param name="agent">���Ӵ�����</param>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        public void Regist(IServerConnectionAgent<BaseMessage> agent)
        {
            if (agent == null) throw new ArgumentNullException("agent");
            lock (_lockObj) _agents.Add(agent);
            agent.Disconnected += Disconnected;
            agent.NewTransaction += NewTransaction;
        }

        /// <summary>
        ///     ע��һ����Ϣ������
        /// </summary>
        /// <param name="protocol">��Ϣ����Э��</param>
        /// <param name="processor">������</param>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        public IRequestScheduler Regist(Protocols protocol, IMessageProcessor processor)
        {
            if (processor == null) throw new ArgumentNullException("processor");
            IMessageProcessor p;
            if (_processors.TryGetValue(protocol, out p))
            {
                _processors.TryUpdate(protocol, processor, p); 
                return this;
            }
            if(!_processors.TryAdd(protocol, processor))throw new Exception("Cannot add a message processor.");
            return this;
        }

        #endregion

        #region Events

        void NewTransaction(object sender, KJFramework.EventArgs.LightSingleArgEventArgs<IMessageTransaction<BaseMessage>> e)
        {
            BusinessMessageTransaction transaction = (BusinessMessageTransaction)e.Target;
            IMessageProcessor processor;
            MessageIdentity identity = transaction.Request.MessageIdentity;
            if (!_processors.TryGetValue(new Protocols { ProtocolId = identity.ProtocolId, ServiceId = identity.ServiceId, DetailsId = identity.DetailsId }, out processor))
            {
                _tracing.Error("#Schedule message failed, because cannot find processor. #P:{0}, S{1}, D{2}", identity.ProtocolId, identity.ServiceId, identity.DetailsId);
                return;
            }
            try { processor.Process(transaction); }
            catch (Exception ex) { _tracing.Error(ex, null);  }
        }

        void Disconnected(object sender, EventArgs e)
        {
            IServerConnectionAgent<BaseMessage> agent = (IServerConnectionAgent<BaseMessage>)sender;
            agent.Disconnected -= Disconnected;
            agent.NewTransaction -= NewTransaction;
            lock (_lockObj) _agents.Remove(agent);
        }

        #endregion
    }
}