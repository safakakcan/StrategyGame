using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Information : ScriptableObject
{
    public string displayName;
    public string description;
    public Sprite sprite;
    public int maxHealth;
    public Vector2Int size;
}
