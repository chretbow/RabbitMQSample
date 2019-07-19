namespace RabbitMQSample.Domain.Model
{
    using System;

    public class PressureTestContentEvent
    {
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
