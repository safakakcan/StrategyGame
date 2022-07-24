using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for Selectable Objects.
/// </summary>
public interface ISelectable
{
    public Information Information { get; }
    public void Select();
}
