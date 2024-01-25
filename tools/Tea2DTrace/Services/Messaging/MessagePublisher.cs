using Microsoft.Extensions.DependencyInjection;

namespace Tea2DTrace.Services.Messaging;

internal class MessagePublisher : IMessagePublisher
{
    private readonly IServiceProvider _serviceProvider;
    
    public MessagePublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : struct, IMessage
    {
        var messageHandlers = _serviceProvider.GetRequiredService<IEnumerable<IMessageHandler<TMessage>>>();

        foreach (var messageHandler in messageHandlers)
        {
            await messageHandler.HandleAsync(message);
        }
    }
}