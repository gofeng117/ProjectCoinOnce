using System.Diagnostics;

namespace CoinAPI.CommonFramework.Objects
{
    [DebuggerDisplay("P: {ProtocolId}, S: {ServiceId}, D: {DetailsId}")]
    public struct Protocols
    {
        public int ProtocolId;
        public int ServiceId;
        public int DetailsId;
    }
}