namespace CodeCreate.Messaging
{
    using System;

    public class SubscriberConfig
    {
        public string? Topic { get; set; }

        public string SubscriberId { get; set; }

        public SubscriberConfig()
        {
            SubscriberId = $"{Guid.NewGuid()}";
        }
    }
}
