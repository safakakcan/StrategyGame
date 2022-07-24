using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic Publisher Class (For Publish & Subscribe pattern)
/// </summary>
/// <typeparam name="T"></typeparam>
public class Publisher<T> : IPublisher<T>
{ 
    public event System.EventHandler<MessageArgument<T>> DataPublisher;

    public void OnDataPublisher(MessageArgument<T> args)
    {
        var handler = DataPublisher;
        if (handler != null)
            handler(this, args);
    }

    /// <summary>
    /// Send Message to all subscribers
    /// </summary>
    /// <param name="data"></param>
    public void PublishData(T data)
    {
        MessageArgument<T> message = (MessageArgument<T>)System.Activator.CreateInstance(typeof(MessageArgument<T>), new object[] { data });
        OnDataPublisher(message);
    }
}
