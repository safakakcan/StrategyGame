using System.Collections;
using System.Collections.Generic;

public interface IPublisher<T>
{
    public event System.EventHandler<MessageArgument<T>> DataPublisher;
    public void OnDataPublisher(MessageArgument<T> args);
    public void PublishData(T data);
}
