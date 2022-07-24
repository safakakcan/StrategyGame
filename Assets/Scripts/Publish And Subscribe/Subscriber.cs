using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic Subscriber Class (For Publish & Subscribe pattern)
/// </summary>
/// <typeparam name="T"></typeparam>
public class Subscriber<T>
{
    public IPublisher<T> Publisher { get; private set; }
    public Subscriber(IPublisher<T> publisher)
    {
        Publisher = publisher;
    }
}