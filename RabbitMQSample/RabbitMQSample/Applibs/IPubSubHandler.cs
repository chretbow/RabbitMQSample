namespace RabbitMQSample.Applibs
{
    using RabbitMQSample.Model;

    public interface IPubSubHandler<TEventStream> where TEventStream : EventStream
    {
        void Handle(TEventStream stream);
    }
}
