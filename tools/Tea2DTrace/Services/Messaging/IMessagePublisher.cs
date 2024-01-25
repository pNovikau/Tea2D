namespace Tea2DTrace.Services.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : struct, IMessage;
}