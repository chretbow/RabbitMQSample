namespace RabbitMQSample2.Handler
{
    using Newtonsoft.Json;
    using RabbitMQSample.Domain.Model;
    using RabbitMQSample2.Model;
    using System;

    public class PressureTestContentEventHandler : IRabbitMQEventStreamHandler
    {
        public void Handle(RabbitMQEventStream stream)
        {
            try
            {
                var @event = JsonConvert.DeserializeObject<PressureTestContentEvent>(stream.Data);
                Console.WriteLine($"PressureTestContent: {@event.Content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PressureTestContentHandler Exception:{ex.Message}");
            }
        }
    }
}
