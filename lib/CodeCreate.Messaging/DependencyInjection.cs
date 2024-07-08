namespace CodeCreate.Messaging
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using EasyNetQ;
    using EasyNetQ.DI;

    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rabbitConnectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddMessaging<TPump>(this IServiceCollection services,
            string rabbitConnectionString) where TPump : MessagePumpBase
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(rabbitConnectionString);

            services.AddSingleton<IMessageBus, RabbitMqMessageBus>();
            services.AddSingleton<IMessagePump, TPump>();

            services.AddSingleton<IBus>(_ =>
            {
                return RabbitHutch.CreateBus(rabbitConnectionString,
                    c =>
                    {
                        //todo check if env == dev to enable console logger
                        c.EnableConsoleLogger();
                        
                        //todo check if system.text.json can handle deserialization to interfaces
                        c.EnableNewtonsoftJson();
                        c.Register<IScheduler, DeadLetterExchangeAndMessageTtlScheduler>();
                    });
            });

            return services;
        }
    }
}
