
namespace RabbitMQSample2.Applibs
{
    using RabbitMQSample2.Model;

    public interface IPubSubDispatcher<TEventStream> where TEventStream : EventStream
    {
        bool DispatchMessage(TEventStream stream);
    }
}
