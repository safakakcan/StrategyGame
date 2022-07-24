using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Factory Class
/// </summary>
public class Factory<T> : MonoBehaviour where T : MonoBehaviour
{
    // Reference to prefab.
    [SerializeField] private T[] prefabs;

    public T[] Prefabs { get { return prefabs; } }

    /// <summary>
    /// Create new instance of prefab by name.
    /// </summary>
    /// <returns>New instance of prefab.</returns>
    public T GetNewInstance(string name)
    {
        foreach (var prefab in prefabs)
        {
            if (prefab.name == name)
                return Instantiate(prefab);
        }

        return null;
    }
}