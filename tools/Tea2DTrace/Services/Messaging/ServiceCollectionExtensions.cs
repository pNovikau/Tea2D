﻿using Microsoft.Extensions.DependencyInjection;

namespace Tea2DTrace.Services.Messaging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection serviceCollection)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);

        serviceCollection.AddSingleton<IMessagePublisher, MessagePublisher>();

        return serviceCollection;
    }

    public static IServiceCollection AddMessageHandler<TMessage, THandler>(this IServiceCollection serviceCollection)
        where TMessage : struct, IMessage
        where THandler : class, IMessageHandler<TMessage>
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);

        serviceCollection.AddSingleton<IMessageHandler<TMessage>, THandler>();

        return serviceCollection;
    }
}