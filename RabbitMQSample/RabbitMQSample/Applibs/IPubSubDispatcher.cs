
namespace RabbitMQSample.Applibs
{
    using RabbitMQSample.Model;

    public interface IPubSubDispatcher<TEventStream> where TEventStream : EventStream
    {
        bool DispatchMessage(TEventStream stream);
    }
}
