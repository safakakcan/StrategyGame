using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILocator
{
    public Vector2Int Position { get; set; }
    public Vector2Int Size { get; }
}
