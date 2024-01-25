namespace Tea2D.Trace.Services.Messaging;

public interface IMessageHandler<in TMessage>
    where TMessage : struct, IMessage
{
    ValueTask HandleAsync(TMessage message);
}