namespace Tea2DTrace.Services.Messaging;

public interface IMessageHandler<in TMessage>
    where TMessage : struct, IMessage
{
    ValueTask HandleAsync(TMessage message);
}