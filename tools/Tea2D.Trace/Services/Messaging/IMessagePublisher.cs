namespace Tea2D.Trace.Services.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : struct, IMessage;
}