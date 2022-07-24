using System.Collections;
using System.Collections.Generic;

/// <summary>
/// MessageArgument Class for Publishers
/// </summary>
/// <typeparam name="T"></typeparam>
public class MessageArgument<T> : System.EventArgs
{
    public T Message { get; set; }
    public MessageArgument(T message)
    {
        Message = message;
    }
}
