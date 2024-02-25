namespace Tea2D.Trace.Services.Messaging;

public class DispatcherMessageHandlerDecorator<TMessage> : IMessageHandler<TMessage>
    where TMessage : struct, IMessage
{
    private readonly IMessageHandler<TMessage> _innerMessageHandler;

    public DispatcherMessageHandlerDecorator(IMessageHandler<TMessage> innerMessageHandler)
    {
        _innerMessageHandler = innerMessageHandler ?? throw new ArgumentNullException(nameof(innerMessageHandler));
    }

    public ValueTask HandleAsync(TMessage message)
    {
        return Application.Current.Dispatcher.CheckAccess() 
            ? _innerMessageHandler.HandleAsync(message) 
            : Application.Current.Dispatcher.Invoke(() => _innerMessageHandler.HandleAsync(message));
    }
}