using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable : ILocator
{
    public Vector2Int Destination { get; set; }
    public int MovementSpeed { get; set; }

    public void Move();
}
