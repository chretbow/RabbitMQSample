namespace RabbitMQSample.Model
{
    public class RabbitMQEventStream : EventStream
    {
        public RabbitMQEventStream(string type, string data, long utcTimeStamp)
        {
            Type = type;
            Data = data;
            UtcTimeStamp = utcTimeStamp;
        }
    }
}
