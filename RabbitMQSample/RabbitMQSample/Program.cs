namespace RabbitMQSample
{
    using RabbitMQSample.Applibs;
    using RabbitMQSample.Domain.Model;
    using RabbitMQSample.Model;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQService.Start(ConfigHelper.RabbitMQURI);

            var consumer = new RabbitMQConsumer(
                ConfigHelper.SubQueueNames,
                new PubSubDispatcher<RabbitMQEventStream>(AutofacConfig.Container),
                ConfigHelper.QueueId);
            consumer.Start();

            Parallel.ForEach(Enumerable.Range(0, ConfigHelper.ProducerBatchCount), (index) =>
            {
                RabbitMQProducer.Publish(
                    ConfigHelper.QueueId,
                    new PressureTestContentEvent()
                    {
                        Content = $"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")}-{index}",
                        CreateDateTime = DateTime.Now
                    });
            });

            Console.Read();

            RabbitMQService.Stop();
        }
    }
}
