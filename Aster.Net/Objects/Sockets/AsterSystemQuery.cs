using CryptoExchange.Net.Sockets;

namespace Aster.Net.Objects.Sockets
{
    internal class AsterSystemQuery<T> : Query<T> where T : AsterSocketQueryResponse
    {
        public AsterSystemQuery(AsterSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<T>(request.Id.ToString());
            MessageRouter = MessageRouter.CreateWithoutHandler<T>(request.Id.ToString());
        }
    }
}
