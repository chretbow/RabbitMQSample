namespace RabbitMQSample2.Model
{
    using RabbitMQSample2.Applibs;
    public interface IRabbitMQEventStreamHandler : IPubSubHandler<RabbitMQEventStream>
    {
    }
}
